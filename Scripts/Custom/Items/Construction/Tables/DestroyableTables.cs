using System;
using Server;
using Server.Mobiles;

namespace Server.Items
{
    public class DestroyableLargeTable : DestroyableItem
    {
        [Constructable]
        public DestroyableLargeTable()
            : base(2960, 2960, 3117)
        {
            Name = "Large Table";
            Weight = 1.0;
            HitsMax = 50;
            Hits = 50;
        }

        public DestroyableLargeTable(Serial serial)
            : base(serial)
        {
        }

        public override void OnDestroyed()
        {

        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            if (Weight == 4.0)
                Weight = 1.0;
        }
    }
}