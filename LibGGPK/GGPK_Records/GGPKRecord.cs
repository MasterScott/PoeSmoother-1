﻿namespace LibGGPK
{
    using System.IO;

    /// <summary>
    /// GGPK record is the very first record and exists at the very beginning of the pack file.
    /// It must have excatly 2 entries - One goes to the root directory and the other to a FREE record.
    /// </summary>
    public sealed class GGPKRecord : BaseRecord
    {
        /// <summary>
        /// List record offsets this record contains. It must have exactly 2 entries.
        /// </summary>
        public long[] RecordOffsets;

        public const string Tag = "GGPK";

        public GGPKRecord(uint length, BinaryReader br)
        {
            RecordBegin = br.BaseStream.Position - 8;
            Length = length;
            Read(br);
        }

        /// <summary>
        /// Reads the GGPK record entry from the specified stream
        /// </summary>
        /// <param name="br">Stream pointing at a GGPK record</param>
        protected override void Read(BinaryReader br)
        {
            int totalRecordOffsets = br.ReadInt32();
            RecordOffsets = new long[totalRecordOffsets];

            for (int i = 0; i < totalRecordOffsets; i++)
            {
                RecordOffsets[i] = br.ReadInt64();
            }
        }

        public override string ToString()
        {
            return Tag;
        }
    }
}