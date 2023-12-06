namespace Common.Routes
{
    public static class ApiRoutes
    {
        private const string Api = "api";

		public static class Root
		{
			public const string BaseRoot = "/";
		}

		public static class Employee
		{
			private const string BaseEmployee = Api + "/employees";
			public const string Get = BaseEmployee;
            public const string Post = BaseEmployee;
            public const string Put = BaseEmployee;
            public const string Delete = BaseEmployee;
        }

        public static class EmployeeShift
        {
            private const string BaseEmployee = Api + "/employee-shifts";
            public const string Post = BaseEmployee;
            public const string Close = BaseEmployee + "/close";
        }

        public static class Suppliers
        {
            private const string BaseEmployee = Api + "/suppliers";
            public const string Get = BaseEmployee;
            public const string Post = BaseEmployee;
            public const string Put = BaseEmployee;
            public const string Delete = BaseEmployee;
        }
    }
}
