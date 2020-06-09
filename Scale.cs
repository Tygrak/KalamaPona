using System;
using System.IO;
using System.Collections.Generic;

namespace KalamaPona {
    public abstract class Scale {
        public abstract int Repetition();
        public abstract float Note(int n);
    }
}
