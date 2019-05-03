namespace ghumpo.data.Infrastructure
{
    public class DatabaseFactory : Disposable, IDatabaseFactory
    {
        private GhumpoContext _dataContext;

        public GhumpoContext Get()
        {
            return _dataContext ?? (_dataContext = new GhumpoContext());
        }

        protected override void DisposeCore()
        {
            _dataContext?.Dispose();
        }
    }
}