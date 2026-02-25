using System;
using NetExtender.Monads;

namespace NetExtender.Types.Monads.Interfaces
{
    public interface IRayId<TSelf> : IRayId, IEqualityStruct<TSelf> where TSelf : struct, IRayId<TSelf>, IEqualityStruct<TSelf>
    {
        public new Boolean IsEmpty { get; }

        public new TSelf Next();
        public new TSelf Next(RayIdPayload? payload);
        public new TSelf Next(Boolean flags);
        public new TSelf Next(Boolean flags, RayIdPayload? payload);
        public new TSelf Next(RayIdFlags flags);
        public new TSelf Next(RayIdFlags flags, RayIdPayload? payload);
        public new TSelf Next<T>(T value) where T : notnull;
        public new TSelf Next<T>(in T value) where T : notnull;
        public new TSelf Next<T>(T value, RayIdPayload? payload) where T : notnull;
        public new TSelf Next<T>(in T value, RayIdPayload? payload) where T : notnull;
        public new TSelf Next<T>(T value, Boolean flags) where T : notnull;
        public new TSelf Next<T>(in T value, Boolean flags) where T : notnull;
        public new TSelf Next<T>(T value, Boolean flags, RayIdPayload? payload) where T : notnull;
        public new TSelf Next<T>(in T value, Boolean flags, RayIdPayload? payload) where T : notnull;
        public new TSelf Next<T>(T value, RayIdFlags flags) where T : notnull;
        public new TSelf Next<T>(in T value, RayIdFlags flags) where T : notnull;
        public new TSelf Next<T>(T value, RayIdFlags flags, RayIdPayload? payload) where T : notnull;
        public new TSelf Next<T>(in T value, RayIdFlags flags, RayIdPayload? payload) where T : notnull;
        public new TSelf Continue();
        public new TSelf Continue(RayIdPayload? payload);
        public new TSelf Continue(Boolean flags);
        public new TSelf Continue(Boolean flags, RayIdPayload? payload);
        public new TSelf Continue(RayIdFlags flags);
        public new TSelf Continue(RayIdFlags flags, RayIdPayload? payload);
        public new TSelf Continue<T>(T value) where T : notnull;
        public new TSelf Continue<T>(in T value) where T : notnull;
        public new TSelf Continue<T>(T value, RayIdPayload? payload) where T : notnull;
        public new TSelf Continue<T>(in T value, RayIdPayload? payload) where T : notnull;
        public new TSelf Continue<T>(T value, Boolean flags) where T : notnull;
        public new TSelf Continue<T>(in T value, Boolean flags) where T : notnull;
        public new TSelf Continue<T>(T value, Boolean flags, RayIdPayload? payload) where T : notnull;
        public new TSelf Continue<T>(in T value, Boolean flags, RayIdPayload? payload) where T : notnull;
        public new TSelf Continue<T>(T value, RayIdFlags flags) where T : notnull;
        public new TSelf Continue<T>(in T value, RayIdFlags flags) where T : notnull;
        public new TSelf Continue<T>(T value, RayIdFlags flags, RayIdPayload? payload) where T : notnull;
        public new TSelf Continue<T>(in T value, RayIdFlags flags, RayIdPayload? payload) where T : notnull;
    }

    public interface IRayId : IRayIdInfo
    {
        public IRayId Next();
        public IRayId Next(RayIdPayload? payload);
        public IRayId Next(Boolean flags);
        public IRayId Next(Boolean flags, RayIdPayload? payload);
        public IRayId Next(RayIdFlags flags);
        public IRayId Next(RayIdFlags flags, RayIdPayload? payload);
        public IRayId Next<T>(T value) where T : notnull;
        public IRayId Next<T>(in T value) where T : notnull;
        public IRayId Next<T>(T value, RayIdPayload? payload) where T : notnull;
        public IRayId Next<T>(in T value, RayIdPayload? payload) where T : notnull;
        public IRayId Next<T>(T value, Boolean flags) where T : notnull;
        public IRayId Next<T>(in T value, Boolean flags) where T : notnull;
        public IRayId Next<T>(T value, Boolean flags, RayIdPayload? payload) where T : notnull;
        public IRayId Next<T>(in T value, Boolean flags, RayIdPayload? payload) where T : notnull;
        public IRayId Next<T>(T value, RayIdFlags flags) where T : notnull;
        public IRayId Next<T>(in T value, RayIdFlags flags) where T : notnull;
        public IRayId Next<T>(T value, RayIdFlags flags, RayIdPayload? payload) where T : notnull;
        public IRayId Next<T>(in T value, RayIdFlags flags, RayIdPayload? payload) where T : notnull;
        public IRayId Continue();
        public IRayId Continue(RayIdPayload? payload);
        public IRayId Continue(Boolean flags);
        public IRayId Continue(Boolean flags, RayIdPayload? payload);
        public IRayId Continue(RayIdFlags flags);
        public IRayId Continue(RayIdFlags flags, RayIdPayload? payload);
        public IRayId Continue<T>(T value) where T : notnull;
        public IRayId Continue<T>(in T value) where T : notnull;
        public IRayId Continue<T>(T value, RayIdPayload? payload) where T : notnull;
        public IRayId Continue<T>(in T value, RayIdPayload? payload) where T : notnull;
        public IRayId Continue<T>(T value, Boolean flags) where T : notnull;
        public IRayId Continue<T>(in T value, Boolean flags) where T : notnull;
        public IRayId Continue<T>(T value, Boolean flags, RayIdPayload? payload) where T : notnull;
        public IRayId Continue<T>(in T value, Boolean flags, RayIdPayload? payload) where T : notnull;
        public IRayId Continue<T>(T value, RayIdFlags flags) where T : notnull;
        public IRayId Continue<T>(in T value, RayIdFlags flags) where T : notnull;
        public IRayId Continue<T>(T value, RayIdFlags flags, RayIdPayload? payload) where T : notnull;
        public IRayId Continue<T>(in T value, RayIdFlags flags, RayIdPayload? payload) where T : notnull;
    }

    public interface IRayIdInfo : IMonad
    {
        public String Format { get; }
        public Boolean HasTimestamp { get; }
        public Boolean IsServerGenerated { get; }
        public Boolean IsUsingHash { get; }
        public Byte Version { get; }
        public (UInt16 Server, UInt16 Service)? Service { get; }
        public Guid Id { get; }
        public UInt64 SpanId { get; }
        public DateTime Timestamp { get; }
        public Guid ParentId { get; }
        public UInt64 ParentSpanId { get; }
        public DateTime ParentTimestamp { get; }
        public RayIdFlags Flags { get; }
        public UInt32? Hash { get; }
        public UInt32 Info { get; }
        public RayIdPayload? Payload { get; }
    }
}