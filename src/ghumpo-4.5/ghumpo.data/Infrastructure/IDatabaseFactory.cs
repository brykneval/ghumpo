using System;

namespace ghumpo.data.Infrastructure
{
    public interface IDatabaseFactory : IDisposable
    {
        GhumpoContext Get();
    }
}