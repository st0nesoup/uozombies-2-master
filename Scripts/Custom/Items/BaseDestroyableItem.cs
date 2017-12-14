using System;
using System.Collections;
using System.Reflection;
using Server;
using Server.Multis;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;
using Server.Targets;

namespace Server.Items
{
    public class DestroyableItem : Item
    {
        private int m_Hits;
        private int m_HitsMax;

        private int m_StartID;
        private int m_DestroyedID;
        private int m_HalfHitsID;

        private IDestroyableItem m_Child;

        [CommandProperty(AccessLevel.GameMaster)]
        public IDestroyableItem Link
        {
            get
            {
                return m_Child;
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int IDStart
        {
            get
            {
                return m_StartID;
            }
            set
            {
                if (value < 0)
                    m_StartID = 0;
                else if (value > int.MaxValue)
                    m_StartID = int.MaxValue;
                else
                    m_StartID = value;

                if (m_Hits >= (m_HitsMax * 0.5))
                {
                    if (ItemID != m_StartID)
                        ItemID = m_StartID;
                }

                InvalidateProperties();
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int IDHalfHits
        {
            get
            {
                return m_HalfHitsID;
            }
            set
            {
                if (value < 0)
                    m_HalfHitsID = 0;
                else if (value > int.MaxValue)
                    m_HalfHitsID = int.MaxValue;
                else
                    m_HalfHitsID = value;

                if (m_Hits < (m_HitsMax * 0.5))
                {
                    if (ItemID != m_HalfHitsID)
                        ItemID = m_HalfHitsID;
                }

                InvalidateProperties();
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int IDDestroyed
        {
            get
            {
                return m_DestroyedID;
            }
            set
            {
                if (value < 0)
                    m_DestroyedID = 0;
                else if (value > int.MaxValue)
                    m_DestroyedID = int.MaxValue;
                else
                    m_DestroyedID = value;

                InvalidateProperties();
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int Hits
        {
            get
            {
                return m_Hits;
            }
            set
            {
                if (value > m_HitsMax)
                    m_Hits = m_HitsMax;
                else
                    m_Hits = value;

                if (m_Child != null && (m_Hits > m_Child.Hits || m_Hits < m_Child.Hits))
                    UpdateHitsToEntity();

                InvalidateProperties();
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int HitsMax
        {
            get
            {
                return m_HitsMax;
            }
            set
            {
                if (value > int.MaxValue)
                    m_HitsMax = int.MaxValue;
                else
                    m_HitsMax = value;

                if (Hits > m_HitsMax)
                    Hits = m_HitsMax;

                if (m_Child != null && (m_HitsMax > m_Child.HitsMax || m_HitsMax < m_Child.HitsMax))
                    UpdateMaxHitsToEntity();

                InvalidateProperties();
            }
        }

		[Constructable]
		public DestroyableItem( int StartID, int HalfID, int DestroyID )
			: base( StartID )
		{
			Name = "Damageable Item";
			Hue = 0;
			Movable = true;

			IDStart = StartID;
			IDHalfHits = HalfID;
			IDDestroyed = DestroyID;
		}

        public virtual void OnDamage(int amount, Mobile from, bool willKill)
        {
            return;
        }

        public virtual bool OnBeforeDestroyed()
        {
            return true;
        }

        public virtual void OnDestroyed()
        {
            return;
        }

        public void UpdateMaxHitsToEntity()
        {
            m_Child.SetHits(HitsMax);
        }

        public void UpdateHitsToEntity()
        {
            m_Child.Hits = Hits;
        }

        public void Damage(int amount, Mobile from, bool willKill)
        {
            if (willKill)
            {
                Destroy();
                return;
            }

            Hits -= amount;

            if (Hits >= (HitsMax * 0.5))
            {
                ItemID = IDStart;
            }
            else if (Hits < (HitsMax * 0.5))
            {
                ItemID = IDHalfHits;
            }
            else if (Hits <= 0)
            {
                Destroy();
                return;
            }

            OnDamage(amount, from, willKill);
        }

        public bool Destroy()
        {
            if (this == null || this.Deleted)
                return false;

            if (OnBeforeDestroyed())
            {
                if (m_Child != null && !m_Child.Deleted && !m_Child.Alive)
                {
                    Rubble rubble = new Rubble(m_DestroyedID, this.Name);

                    if (rubble != null && !rubble.Deleted && this.Map != null && this.Map != Map.Internal)
                    {
                        rubble.MoveToWorld(this.Location, this.Map);
                    }

                    OnDestroyed();

                    if (m_Child != null)
                        m_Child.Delete();

                    Delete();

                    return true;
                }
                else
                {
                    Rubble rubble = new Rubble(m_DestroyedID, this.Name);
                    //Static rubble = new Static(m_DestroyedID);

                    if (rubble != null && !rubble.Deleted && this.Map != null && this.Map != Map.Internal)
                        rubble.MoveToWorld(this.Location, this.Map);

                    OnDestroyed();

                    Delete();
                    return true;
                }
            }

            return false;
        }

        private void ProvideEntity()
        {
            if (m_Child != null)
            {
                m_Child.Delete();
            }

            IDestroyableItem Idam = new IDestroyableItem(this);

            if (Idam != null && !Idam.Deleted && this.Map != null)
            {
                m_Child = Idam;
                m_Child.Update();
            }
        }

        public override void OnLocationChange(Point3D oldLocation)
        {
            if (this.Location != oldLocation)
            {
                if (m_Child != null && !m_Child.Deleted)
                {
                    if (m_Child.Location == oldLocation)
                        m_Child.Update();
                }
            }

            base.OnLocationChange(oldLocation);
        }

        public override bool CheckTarget(Mobile from, Target targ, object targeted)
        {
            #region CheckEntity

            //Check to see if we have an Entity Link (Child BaseCreature)
            //If not, create one!
            //(Without Combatant Change, since this is for pets)

            PlayerMobile pm = from as PlayerMobile;

            if (pm != null)
            {
                if (m_Child != null && !m_Child.Deleted)
                {
                    m_Child.Update();
                }
                else
                {
                    ProvideEntity();

                    if (m_Child != null && !m_Child.Deleted)
                    {
                        m_Child.Update();
                    }
                }
            }
            #endregion

            if (targ is AIControlMobileTarget && targeted == this)
            {
                //Wrap the target
                AIControlMobileTarget t = targ as AIControlMobileTarget;
                //Get the OrderType
                OrderType order = t.Order;

                //Search for our controlled pets within screen range
                foreach (Mobile m in from.GetMobilesInRange(16))
                {
                    if (!(m is BaseCreature))
                        continue;

                    BaseCreature bc = m as BaseCreature;

                    if (from != null && !from.Deleted && from.Alive)
                    {
                        if (bc == null || bc.Deleted || !bc.Alive || !bc.Controlled || bc.ControlMaster != from)
                            continue;

                        //Reset the pet's ControlTarget and OrderType.
                        bc.ControlTarget = m_Child;
                        bc.ControlOrder = t.Order;
                    }
                }
            }

            return base.CheckTarget(from, targ, targeted);
        }

        public override void OnDoubleClick(Mobile from)
        {
            Attack(from);

            base.OnDoubleClick(from);
        }

        protected void Attack(Mobile from)
        {
            PlayerMobile pm = from as PlayerMobile;

            if (pm != null)
            {
                if (m_Child != null && !m_Child.Deleted)
                {
                    m_Child.Update();
                    pm.Warmode = true;
                    pm.Combatant = m_Child;
                }
                else
                {
                    ProvideEntity();
            
                    if (m_Child != null && !m_Child.Deleted)
                    {
                        m_Child.Update();
                        pm.Warmode = true;
                        pm.Combatant = m_Child;
                    }
                }
            } 
        }

        public override bool OnDragLift(Mobile from)
        {
            return (from.AccessLevel >= AccessLevel.Counselor);
        }

        public override void Delete()
        {
            base.Delete();

            if (m_Child != null && !m_Child.Deleted)
            {
                m_Child.Delete();
                return;
            }
        }

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			ArrayList strings = new ArrayList( );

			strings.Add( "-Strength-" );
			strings.Add( m_Hits + "/" + m_HitsMax );

			string toAdd = "";
			int amount = strings.Count;
			int current = 1;

			foreach( string str in strings )
			{
				toAdd += str;

				if( current != amount )
					toAdd += "\n";

				++current;
			}

			if( toAdd != "" )
				list.Add( 1070722, toAdd );
		}

		public DestroyableItem( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( ( int )0 ); // version

			writer.Write( ( Mobile )m_Child );
			writer.Write( ( int )m_StartID );
			writer.Write( ( int )m_HalfHitsID );
			writer.Write( ( int )m_DestroyedID );
			writer.Write( ( int )m_Hits );
			writer.Write( ( int )m_HitsMax );
			writer.Write( ( bool )Movable );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt( );

			m_Child = ( IDestroyableItem )reader.ReadMobile( );
			m_StartID = ( int )reader.ReadInt( );
			m_HalfHitsID = ( int )reader.ReadInt( );
			m_DestroyedID = ( int )reader.ReadInt( );
			m_Hits = ( int )reader.ReadInt( );
			m_HitsMax = ( int )reader.ReadInt( );
			Movable = ( bool )reader.ReadBool( );
		}
    }

    public class IDestroyableItem : BaseCreature
    {
		private DestroyableItem m_Parent;

		[CommandProperty( AccessLevel.GameMaster )]
		public DestroyableItem Link
		{
			get
			{
				return m_Parent;
			}
		}

		[Constructable]
		public IDestroyableItem( DestroyableItem parent )
			: base( AIType.AI_Melee, FightMode.None, 1, 1, 0.2, 0.4 )
		{
			if( parent != null && !parent.Deleted )
				m_Parent = parent;
				
			Name = null;

            Body = 906;
            BodyValue = 906;
			Hue = 0;
			BaseSoundID = 0;
			Fame = 0;
			Karma = 0;
			ControlSlots = 0;
			Tamable = false;

			Frozen = true;
			Paralyzed = true;
			CantWalk = true;

			DamageMin = 0;
			DamageMax = 0;

			SetStr( m_Parent.HitsMax );
			SetHits( m_Parent.HitsMax );
			Hits = m_Parent.Hits;

			for( int skill = 0; skill < this.Skills.Length; skill++ )
			{
				this.Skills[( SkillName )skill].Cap = 0.0;
				this.Skills[( SkillName )skill].Base = 0.0;
			}

			Update( );
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			ArrayList strings = new ArrayList( );

			strings.Add( "-Strength-" );
			strings.Add( m_Parent.Hits + "/" + m_Parent.HitsMax );

			string toAdd = "";
			int amount = strings.Count;
			int current = 1;

			foreach( string str in strings )
			{
				toAdd += str;

				if( current != amount )
					toAdd += "\n";

				++current;
			}

			if( toAdd != "" )
				list.Add( 1070722, toAdd );
		}

		public void Update( )
		{
			if( this == null || this.Deleted )
				return;

			if( m_Parent != null && !m_Parent.Deleted )
			{
				this.Home = m_Parent.Location;
				this.Location = m_Parent.Location;
				this.Map = m_Parent.Map;

				return;
			}

			if( m_Parent == null || m_Parent.Deleted )
			{
				Delete( );
				return;
			}
		}

		public override void Delete( )
		{
			base.Delete( );
		}

		public override void OnDamage( int amount, Mobile from, bool willKill )
		{
			base.OnDamage( amount, from, willKill );

			if( m_Parent != null && !m_Parent.Deleted )
			{
				m_Parent.Damage( amount, from, willKill );
			}
		}

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );

			if( m_Parent != null && !m_Parent.Deleted )
				m_Parent.Destroy( );
		}

		public IDestroyableItem( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( ( int )0 );

			writer.Write( ( Item )m_Parent );
			writer.Write( ( bool )Frozen );
			writer.Write( ( bool )Paralyzed );
			writer.Write( ( bool )CantWalk );
			writer.Write( ( int )DamageMin );
			writer.Write( ( int )DamageMax );
			writer.Write( ( int )BodyValue );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt( );

			m_Parent = ( DestroyableItem )reader.ReadItem( );
			Frozen = ( bool )reader.ReadBool( );
			Paralyzed = ( bool )reader.ReadBool( );
			CantWalk = ( bool )reader.ReadBool( );
			DamageMin = ( int )reader.ReadInt( );
			DamageMax = ( int )reader.ReadInt( );
			BodyValue = ( int )reader.ReadInt( );
		}
    }
}