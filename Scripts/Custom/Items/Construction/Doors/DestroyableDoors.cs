using System;

namespace Server.Items
{
    public class DestroyableIronGateShort : BaseDestroyableDoor
    {
        [Constructable]
        public DestroyableIronGateShort(DoorFacing facing)
            : base(0x84c + (2 * (int)facing), 0x84d + (2 * (int)facing), 0xEC, 0xF3, BaseDestroyableDoor.GetOffset(facing))
        {
            Name = "Short Iron Gate";
            Weight = 1.0;
            HitsMax = 50;
            Hits = 50;
        }

        public DestroyableIronGateShort(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer) // Default Serialize method
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader) // Default Deserialize method
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class DestroyableIronGate : BaseDestroyableDoor
    {
        [Constructable]
        public DestroyableIronGate(DoorFacing facing)
            : base(0x824 + (2 * (int)facing), 0x825 + (2 * (int)facing), 0xEC, 0xF3, BaseDestroyableDoor.GetOffset(facing))
        {
            Name = "Iron Gate";
            Weight = 1.0;
            HitsMax = 50;
            Hits = 50;
        }

        public DestroyableIronGate(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer) // Default Serialize method
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader) // Default Deserialize method
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class DestroyableLightWoodGate : BaseDestroyableDoor
    {
        [Constructable]
        public DestroyableLightWoodGate(DoorFacing facing)
            : base(0x839 + (2 * (int)facing), 0x83A + (2 * (int)facing), 0xEB, 0xF2, BaseDestroyableDoor.GetOffset(facing))
        {
            Name = "Light Wood Gate";
            Weight = 1.0;
            HitsMax = 50;
            Hits = 50;
        }

        public DestroyableLightWoodGate(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer) // Default Serialize method
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader) // Default Deserialize method
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class DestroyableDarkWoodGate : BaseDestroyableDoor
    {
        [Constructable]
        public DestroyableDarkWoodGate(DoorFacing facing)
            : base(0x866 + (2 * (int)facing), 0x867 + (2 * (int)facing), 0xEB, 0xF2, BaseDestroyableDoor.GetOffset(facing))
        {
            Name = "Dark Wood Gate";
            Weight = 1.0;
            HitsMax = 50;
            Hits = 50;
        }

        public DestroyableDarkWoodGate(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer) // Default Serialize method
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader) // Default Deserialize method
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class DestroyableMetalDoor : BaseDestroyableDoor
    {
        [Constructable]
        public DestroyableMetalDoor(DoorFacing facing)
            : base(0x675 + (2 * (int)facing), 0x676 + (2 * (int)facing), 0xEC, 0xF3, BaseDestroyableDoor.GetOffset(facing))
        {
            Name = "Metal Door";
            Weight = 1.0;
            HitsMax = 50;
            Hits = 50;
        }

        public DestroyableMetalDoor(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer) // Default Serialize method
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader) // Default Deserialize method
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class DestroyableBarredMetalDoor : BaseDestroyableDoor
    {
        [Constructable]
        public DestroyableBarredMetalDoor(DoorFacing facing)
            : base(0x685 + (2 * (int)facing), 0x686 + (2 * (int)facing), 0xEC, 0xF3, BaseDestroyableDoor.GetOffset(facing))
        {
            Name = "Barred Metal Door";
            Weight = 1.0;
            HitsMax = 50;
            Hits = 50;
        }

        public DestroyableBarredMetalDoor(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer) // Default Serialize method
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader) // Default Deserialize method
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class DestroyableBarredMetalDoor2 : BaseDestroyableDoor
    {
        [Constructable]
        public DestroyableBarredMetalDoor2(DoorFacing facing)
            : base(0x1FED + (2 * (int)facing), 0x1FEE + (2 * (int)facing), 0xEC, 0xF3, BaseDestroyableDoor.GetOffset(facing))
        {
            Name = "Barred Metal Door";
            Weight = 1.0;
            HitsMax = 50;
            Hits = 50;
        }

        public DestroyableBarredMetalDoor2(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer) // Default Serialize method
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader) // Default Deserialize method
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class DestroyableRattanDoor : BaseDestroyableDoor
    {
        [Constructable]
        public DestroyableRattanDoor(DoorFacing facing)
            : base(0x695 + (2 * (int)facing), 0x696 + (2 * (int)facing), 0xEB, 0xF2, BaseDestroyableDoor.GetOffset(facing))
        {
            Name = "Rattan Door";
            Weight = 1.0;
            HitsMax = 50;
            Hits = 50;
        }

        public DestroyableRattanDoor(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer) // Default Serialize method
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader) // Default Deserialize method
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class DestroyableDarkWoodDoor : BaseDestroyableDoor
    {
        [Constructable]
        public DestroyableDarkWoodDoor(DoorFacing facing)
            : base(0x6A5 + (2 * (int)facing), 0x6A6 + (2 * (int)facing), 0xEA, 0xF1, BaseDestroyableDoor.GetOffset(facing))
        {
            Name = "Dark Wood Door";
            Weight = 1.0;
            HitsMax = 50;
            Hits = 50;
        }

        public DestroyableDarkWoodDoor(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer) // Default Serialize method
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader) // Default Deserialize method
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class DestroyableMediumWoodDoor : BaseDestroyableDoor
    {
        [Constructable]
        public DestroyableMediumWoodDoor(DoorFacing facing)
            : base(0x6B5 + (2 * (int)facing), 0x6B6 + (2 * (int)facing), 0xEA, 0xF1, BaseDestroyableDoor.GetOffset(facing))
        {
            Name = "Medium Wood Door";
            Weight = 1.0;
            HitsMax = 50;
            Hits = 50;
        }

        public DestroyableMediumWoodDoor(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer) // Default Serialize method
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader) // Default Deserialize method
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class DestroyableMetalDoor2 : BaseDestroyableDoor
    {
        [Constructable]
        public DestroyableMetalDoor2(DoorFacing facing)
            : base(0x6C5 + (2 * (int)facing), 0x6C6 + (2 * (int)facing), 0xEC, 0xF3, BaseDestroyableDoor.GetOffset(facing))
        {
            Name = "Metal Door";
            Weight = 1.0;
            HitsMax = 50;
            Hits = 50;
        }

        public DestroyableMetalDoor2(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer) // Default Serialize method
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader) // Default Deserialize method
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class DestroyableLightWoodDoor : BaseDestroyableDoor
    {
        [Constructable]
        public DestroyableLightWoodDoor(DoorFacing facing)
            : base(0x6D5 + (2 * (int)facing), 0x6D6 + (2 * (int)facing), 0xEA, 0xF1, BaseDestroyableDoor.GetOffset(facing))
        {
            Name = "Light Wood Door";
            Weight = 1.0;
            HitsMax = 50;
            Hits = 50;
        }

        public DestroyableLightWoodDoor(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer) // Default Serialize method
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader) // Default Deserialize method
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class DestroyableStrongWoodDoor : BaseDestroyableDoor
    {
        [Constructable]
        public DestroyableStrongWoodDoor(DoorFacing facing)
            : base(0x6E5 + (2 * (int)facing), 0x6E6 + (2 * (int)facing), 0xEA, 0xF1, BaseDestroyableDoor.GetOffset(facing))
        {
            Name = "Strong Wood Door";
            Weight = 1.0;
            HitsMax = 50;
            Hits = 50;
        }

        public DestroyableStrongWoodDoor(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer) // Default Serialize method
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader) // Default Deserialize method
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}