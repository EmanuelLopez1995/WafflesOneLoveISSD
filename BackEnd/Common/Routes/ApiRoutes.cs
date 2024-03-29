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
            public const string GetAll = BaseEmployee + "/get-all";
            public const string Get = BaseEmployee;
            public const string Post = BaseEmployee;
            public const string Put = BaseEmployee;
            public const string Delete = BaseEmployee;
        }

        public static class PaymentBox
        {
            private const string BasePaymentBox = Api + "/paymentBox";
            public const string GetAll = BasePaymentBox + "/get-all";
            public const string Get = BasePaymentBox;
            public const string Post = BasePaymentBox;
            public const string Put = BasePaymentBox;
            public const string Delete = BasePaymentBox;
        }

        public static class EmployeeShift
        {
            private const string BaseEmployeeShift = Api + "/employee-shifts";
            public const string Post = BaseEmployeeShift;
            public const string Close = BaseEmployeeShift + "/close";
        }

        public static class Suppliers
        {
            private const string BaseSuppliers = Api + "/suppliers";
            public const string GetAll = BaseSuppliers + "/get-all";
            public const string Get = BaseSuppliers;
            public const string Post = BaseSuppliers;
            public const string Put = BaseSuppliers;
            public const string Delete = BaseSuppliers;
        }

        public static class Shift
        {
            private const string BaseShifts = Api + "/shifts";
            public const string GetAll = BaseShifts + "/get-all";
            public const string Get = BaseShifts;
            public const string Post = BaseShifts;
            public const string Put = BaseShifts;
            public const string Delete = BaseShifts;
            public const string Close = BaseShifts + "/close";
        }
        public static class Salary
        {
            private const string BaseSalary = Api + "/salary";
            public const string GetAll = BaseSalary + "/get-all";
            public const string Get = BaseSalary;
            public const string Post = BaseSalary;
            public const string Put = BaseSalary;
            public const string Delete = BaseSalary;
        }
    }
}
