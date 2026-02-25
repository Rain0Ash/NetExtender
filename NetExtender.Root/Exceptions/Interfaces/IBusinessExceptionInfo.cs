using System;
using System.Collections;
using System.Collections.Immutable;
using System.Net;
using System.Runtime.CompilerServices;
using NetExtender.Monads;

namespace NetExtender.Exceptions.Interfaces
{
    public interface IBusinessExceptionInfo<out T> : IBusinessExceptionInfo
    {
        public T Code { get; }
    }

    public interface IBusinessExceptionInfo
    {
        public Guid Id { get; }
        public RayIdContext RayId { get; }
        public String? Message { get; }
        public String? Identity { get; }
        public IDictionary Data { get; }
        public String? Description { get; }
        public DateTime? When { get; }
        public TimeSpan? RetryAfter { get; }
        public HttpStatusCode? Status { get; }
        public Boolean Business { get; }
        public IBusinessExceptionInfo? Inner { get; }
        public ImmutableList<IBusinessExceptionInfo>? Reason { get; }

        public void Deconstruct(out Guid id, out RayIdContext context, out String? message, out String? name, out String? description, out DateTime? when, out TimeSpan? retry, out HttpStatusCode? status);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Deconstruct(out Guid id, out RayIdContext context, out String? message, out String? name, out String? description, out DateTime? when, out TimeSpan? retry, out HttpStatusCode? status, out IBusinessExceptionInfo? reason)
        {
            Deconstruct(out id, out context, out message, out name, out description, out when, out retry, out status);
            reason = Inner;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Deconstruct(out Guid id, out RayIdContext context, out String? message, out String? name, out String? description, out DateTime? when, out TimeSpan? retry, out HttpStatusCode? status, out IBusinessExceptionInfo? reason, out ImmutableList<IBusinessExceptionInfo>? inner)
        {
            Deconstruct(out id, out context, out message, out name, out description, out when, out retry, out status, out reason);
            inner = Reason;
        }
    }
}