using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject {
    /// <summary>
    /// Класс который описывает симплекс таблицу
    /// </summary>
    class SimplexTable {
        //Коэффициенты целевой функции при направляющих строках
        public double[] Cs { get {
                int leng = fs.GetLength(0);
                double[] Cs = new double[leng];
                for (int i = 0; i < leng; i++) {
                    Cs[i] = C[fs[i] - 1];
                }
                return Cs;
            }}
        //Индексы направляющих строк
        public int[] fs {
            get {
                int leng = X.GetLength(0);
                int[] fs = new int[leng];
                for (int i = 0; i < leng; i++) {
                    fs[i] = (int)X[i,0];
                }
                return fs; 
            }}
        //Опорный план
        public double[,] X { get; set; }
        //Параметр для определения номера направляющей строки
        public double[] teta { get; set; }
        //Оценка разложений вектора условий по базису опорного плана
        public double[] delta { get; set; }
        //Направляющая строка
        public int r { get; set; }
        //Направляющий столбец
        public int k { get; set; }
        //Коэффициенты целевой функции
        public double[] C { get; set; }
        //Значение целевой функции на данном базисе
        public double L { get; set; }
        //Ситуация на данном базисе
        public int situation { get; set; }

        public SimplexTable() { }

    }


}
