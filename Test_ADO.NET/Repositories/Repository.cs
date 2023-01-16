namespace Test_ADO.NET.Repositories
{
    public abstract class Repository<T>
    {
        protected string ConStr => @"Data Source=SGORBATIUK-NB;Initial Catalog=UNI_TEST;Integrated Security=True;Connect Timeout=30;
           Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public abstract T GetById(int id);

        public abstract List<T> Get();

        public abstract int Add(T item);

        public abstract void Update(int id, T item);


    }
}
