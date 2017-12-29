namespace OfficeDep
{
    public static class Session
    {
        public static int page = 0;
        public static bool suiteEnabled = true;
        public static SuiteSKU sku = SuiteSKU.Home;
        public static bool visioEnabled = false;
        public static bool projectEnabled = false;
        public static Pathway pathway = Pathway.Quick;
        public static Architect arch = Architect.x86;
        public static UpdateCycle updChannel = UpdateCycle.Monthly;
        public static string installSource = "";
        public static bool[] appsMap = new bool[10];
        public static string lang = "";

        public delegate Operation ConfFileWriter(string path);
        public static ConfFileWriter confWriter;

        public enum SuiteSKU
        {
            Home, ProPlus
        }
        public enum Pathway
        {
            Quick, Custom, Download
        }
        public enum Architect
        {
            x86, x64
        }
        public enum UpdateCycle
        {
            Monthly = 0, Broad = 1, Targeted = 2
        }
    }
}
