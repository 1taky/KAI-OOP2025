using System;
using MemoryPack;

namespace Lab3_Serialization;

[MemoryPackable]
public partial class StringEntity
{
    public string Value { get; set; } = string.Empty;
    public int Length => Value.Length;
    public StringEntity() { }
    [MemoryPackConstructor]
    public StringEntity(string value) => Value = value;

    public override string ToString() => $"{Value} (length: {Length})";
}