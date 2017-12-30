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
        public static bool[] langSelection = new bool[40];
        public static string[] langList = new string[]
        {
            "ar-sa","bg-bg","zh-cn","zh-tw","hr-hr",
            "cs-cz","da-dk","nl-nl","en-us","et-ee",
            "fi-fi","fr-fr","de-de","el-gr","he-il",
            "hi-in","hu-hu","id-id","it-it","ja-jp",
            "kk-kz","ko-kr","lv-lv","lt-lt","ms-my",
            "nb-no","pl-pl","pt-br","pt-pt","ro-ro",
            "ru-ru","sr-latn-rs","sk-sk","sl-si","es-es",
            "sv-se","th-th","tr-tr","uk-ua","vi-vn",
        };


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
