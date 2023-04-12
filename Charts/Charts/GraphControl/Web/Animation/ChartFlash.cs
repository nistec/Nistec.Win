namespace Nistec.Charts.Web
{
    using System;

    [Serializable]
    internal class ChartFlash
    {
        private int backgroundStartupAnimationDuration = 1;
        private int elementsMouseOverAnimationDuration = 1;
        private AnimationSoft elementsMouseOverAnimationSoft = AnimationSoft.SoftInOut;
        private AnimationMode elementsMouseOverAnimationMode = AnimationMode.Strong;
        private AnimationType elementsMouseOverAnimationType = AnimationType.Drop;
        private int elementsMouseOverAnimationValueChange = -30;
        private int elementsStartupAnimationDuration = 3;
        private AnimationSoft elementsStartupAnimationSoft = AnimationSoft.SoftOut;
        private AnimationMode elementsStartupAnimationMode = AnimationMode.Jumping;
        private AnimationOrder elementsStartupAnimationOrder = AnimationOrder.OneByOne;
        private AnimationType elementsStartupAnimationType = AnimationType.Drop;
        private int keyStartupAnimationDuration = 3;
        private AnimationSoft keyStartupAnimationSoft = AnimationSoft.SoftOut;
        private AnimationMode keyStartupAnimationMode = AnimationMode.Jumping;
        private AnimationOrder keyStartupAnimationOrder = AnimationOrder.OneByOne;
        private AnimationType keyStartupAnimationType = AnimationType.Drop;
        private int titleStartupAnimationDuration = 3;
        private AnimationSoft titleStartupAnimationSoft = AnimationSoft.SoftOut;
        private AnimationMode titleStartupAnimationMode = AnimationMode.Jumping;
        private AnimationType titleStartupAnimationType = AnimationType.Drop;
        private bool useFlash;

        public static string GetObject(int width, int height, string id, string urlObiect, string xml)
        {
            string format = "            \r\n<object classid=\"clsid:d27cdb6e-ae6d-11cf-96b8-444553540000\" codebase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,0,0\" width=\"{0}\" height=\"{1}\" id=\"{2}\" align=\"middle\">\r\n    <param name=\"allowScriptAccess\" value=\"sameDomain\" />\r\n    <param name=\"movie\" value=\"{3}&xml={4}&XWidth={0}&XHeight={1}\" />\r\n    <param name=\"quality\" value=\"best\" />\r\n    <param name=\"loop\" value=\"false\" />\r\n    <param name=\"menu\" value=\"false\" /> \r\n    <param name=\"salign\" value=\"lt\" />    \r\n    <param name=\"bgcolor\" value=\"#ffffff\" />\r\n    <param name=\"scale\" value=\"noscale\" />\r\n    <embed src=\"{3}&xml={4}&XWidth={0}&XHeight={1}\" quality=\"best\" loop=\"false\" salign=\"lt\" menu=\"false\" bgcolor=\"#ffffff\" scale=\"noscale\" align=\"middle\" width=\"{0}\" height=\"{1}\" name=\"{2}\" allowScriptAccess=\"sameDomain\" type=\"application/x-shockwave-flash\" pluginspage=\"http://www.macromedia.com/go/Getflashplayer\" />\r\n</object>";
            return string.Format(format, new object[] { width, height, id, urlObiect, xml });
        }

        public int AnimationBackgroundStartupDuration
        {
            get
            {
                return this.backgroundStartupAnimationDuration;
            }
            set
            {
                this.backgroundStartupAnimationDuration = value;
            }
        }

        public int AnimationElementsMouseOverDuration
        {
            get
            {
                return this.elementsMouseOverAnimationDuration;
            }
            set
            {
                this.elementsMouseOverAnimationDuration = value;
            }
        }

        public AnimationSoft AnimationElementsMouseOverSoft
        {
            get
            {
                return this.elementsMouseOverAnimationSoft;
            }
            set
            {
                this.elementsMouseOverAnimationSoft = value;
            }
        }

        public AnimationMode AnimationElementsMouseOverMode
        {
            get
            {
                return this.elementsMouseOverAnimationMode;
            }
            set
            {
                this.elementsMouseOverAnimationMode = value;
            }
        }

        public AnimationType AnimationElementsMouseOverType
        {
            get
            {
                return this.elementsMouseOverAnimationType;
            }
            set
            {
                this.elementsMouseOverAnimationType = value;
            }
        }

        public int AnimationElementsMouseOverValueChange
        {
            get
            {
                return this.elementsMouseOverAnimationValueChange;
            }
            set
            {
                this.elementsMouseOverAnimationValueChange = value;
            }
        }

        public int AnimationElementsStartupDuration
        {
            get
            {
                return this.elementsStartupAnimationDuration;
            }
            set
            {
                this.elementsStartupAnimationDuration = value;
            }
        }

        public AnimationSoft AnimationElementsStartupSoft
        {
            get
            {
                return this.elementsStartupAnimationSoft;
            }
            set
            {
                this.elementsStartupAnimationSoft = value;
            }
        }

        public AnimationMode AnimationElementsStartupMode
        {
            get
            {
                return this.elementsStartupAnimationMode;
            }
            set
            {
                this.elementsStartupAnimationMode = value;
            }
        }

        public AnimationOrder AnimationElementsStartupOrder
        {
            get
            {
                return this.elementsStartupAnimationOrder;
            }
            set
            {
                this.elementsStartupAnimationOrder = value;
            }
        }

        public AnimationType AnimationElementsStartupType
        {
            get
            {
                return this.elementsStartupAnimationType;
            }
            set
            {
                this.elementsStartupAnimationType = value;
            }
        }

        public int AnimationKeyStartupDuration
        {
            get
            {
                return this.keyStartupAnimationDuration;
            }
            set
            {
                this.keyStartupAnimationDuration = value;
            }
        }

        public AnimationSoft AnimationKeyStartupSoft
        {
            get
            {
                return this.keyStartupAnimationSoft;
            }
            set
            {
                this.keyStartupAnimationSoft = value;
            }
        }

        public AnimationMode AnimationKeyStartupMode
        {
            get
            {
                return this.keyStartupAnimationMode;
            }
            set
            {
                this.keyStartupAnimationMode = value;
            }
        }

        public AnimationOrder AnimationKeyStartupOrder
        {
            get
            {
                return this.keyStartupAnimationOrder;
            }
            set
            {
                this.keyStartupAnimationOrder = value;
            }
        }

        public AnimationType AnimationKeyStartupType
        {
            get
            {
                return this.keyStartupAnimationType;
            }
            set
            {
                this.keyStartupAnimationType = value;
            }
        }

        public int AnimationTitleStartupDuration
        {
            get
            {
                return this.titleStartupAnimationDuration;
            }
            set
            {
                this.titleStartupAnimationDuration = value;
            }
        }

        public AnimationSoft AnimationTitleStartupSoft
        {
            get
            {
                return this.titleStartupAnimationSoft;
            }
            set
            {
                this.titleStartupAnimationSoft = value;
            }
        }

        public AnimationMode AnimationTitleStartupMode
        {
            get
            {
                return this.titleStartupAnimationMode;
            }
            set
            {
                this.titleStartupAnimationMode = value;
            }
        }

        public AnimationType AnimationTitleStartupType
        {
            get
            {
                return this.titleStartupAnimationType;
            }
            set
            {
                this.titleStartupAnimationType = value;
            }
        }

        public bool UseFlash
        {
            get
            {
                return this.useFlash;
            }
            set
            {
                this.useFlash = value;
            }
        }
    }
}

