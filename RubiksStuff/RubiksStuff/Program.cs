using System;
using System.IO;
using System.Numerics;
using System.Collections;
using System.Runtime.CompilerServices;

namespace RubiksStuff
{
    class Program
    {


        static void Main(string[] args)
        {
            string line2 = "2,3,5,7,11,13,17,19,23,29,31,37,41,43,47,53,59,61,67,71,73,79,83,89,97,101,103,107,109,113,127,131,137,139,149,151,157,163,167,173,179,181,191,193,197,199,211,223,227,229,233,239,241,251,257,263,269,271,277,281,283,293,307,311,313,317,331,337,347,349,353,359,367,373,379,383,389,397,401,409,419,421,431,433,439,443";
            string[] things2 = line2.Split(",");
            int fool = things2.Length;
            int[] primes2 = new int[fool];
            for (int ai = 0; ai < fool; ai++)
            {
                primes2[ai] = int.Parse(things2[ai]);
            }
            Console.WriteLine("Hello World!");
            int foo = 2;
            if (foo == 0)
            {
                RunStuff1 rs1 = new RunStuff1();
                rs1.DoIt(primes2);
                DataFileMaker dfm = new DataFileMaker();
                dfm.ReadAllRuns();
            }
            else if (foo == 1)
            {
                RunStuff0 rs0 = new RunStuff0();
                rs0.DoStuff2(1, 3, 15, primes2, 100, 10000);
            }
            else if (foo == 2)
            {
                ParamLoader loadz = new ParamLoader();
                loadz.LoadIt(primes2);
            }
        }
    }
    class HardWay
    {
        private System.Random random;
        private int smally;
        private bool isorig;
        private SMap cube2;
        public void StartIt(int nfin)
        {
            cube2 = new SMap();
            cube2.StartIt(nfin);
            cube2.ResetCubes();

        }
        public void DoSingle(int[] maporigin)
        {
            isorig = false;
            cube2.SetCMap(maporigin);
            smally = 1;
            while (!isorig)
            {
                cube2.Mapo();
                isorig = cube2.GetOrig();
                if (!isorig)
                {
                    smally++;
                }
            }
            Console.WriteLine("SMALLY: " + smally);

        }
    }
    class DirectorySetup
    {
        private string direcpath;
        public void SetupDirectoryPath(string direcpathin)
        {

        }
    }
    class ParamLoader
    {
        private string line, path;
        private int nls;
        private RunStuff1 rss1;
        private string[] things, lines;
        public void LoadIt(int[] primes2in)
        {
            /*
             * Input format: type d nmin nmax lmin lmax dt dist
             */
            Console.WriteLine("Enter param file number below:");
            line = Console.ReadLine();
            path = @"C:\rubiks\standard\cstuff\params\par" + line + ".dat";
            lines = System.IO.File.ReadAllLines(path);
            nls = lines.Length;
            for (int yyy = 0; yyy < nls; yyy++)
            {
                rss1 = new RunStuff1();
                rss1.DoIt3(lines[yyy], primes2in);
            }
        }
    }
    class DataFileMaker
    {
        private string[] things, lines, things2, lines2;
        private string line, path3, path4;
        private int fcount, nlines, nsubruns, punt, i0, i1, nt, nmaxrecl, maxprime, type, d, n, l, nlabed;
        private ArrayList labels, labeled, scores0, scores1;
        private long dt, dong;
        private BigInteger maxrecl;
        private string path2, path, line0, line1, templab;
        private LabeledThing thing;
        /*
         * Labels denote the "type d n l" string unique with each labeled.
         */
        public void ReadAllRuns()
        {
            path2 = @"C:\rubiks\standard\cstuff\runs\";
            labels = new ArrayList();
            labeled = new ArrayList();
            scores0 = new ArrayList();
            scores1 = new ArrayList();
            fcount = Directory.GetFiles(path2, "*", SearchOption.TopDirectoryOnly).Length;
            for (int aaa = 0; aaa < fcount; aaa++)
            {
                path = path2 + "run_" + aaa + ".dat";
                lines = System.IO.File.ReadAllLines(path);
                nlines = lines.Length;
                nsubruns = nlines - 1;
                nsubruns = nsubruns / 2;
                punt = 1;
                for (int bbb = 0; bbb < nsubruns; bbb++)
                {
                    line0 = lines[punt];
                    punt++;
                    line1 = lines[punt];
                    punt++;
                    things2 = line1.Split(" ");
                    things = line0.Split(" ");
                    nt = int.Parse(things[4]);
                    maxrecl = BigInteger.Parse(things[5]);
                    nmaxrecl = int.Parse(things[6]);
                    dt = long.Parse(things[7]);
                    maxprime = int.Parse(things[8]);
                    templab = things[0] + " " + things[1] + " " + things[2] + " " + things[3];
                    Console.WriteLine("Debug2: " + templab);
                    type = int.Parse(things[0]);
                    d = int.Parse(things[1]);
                    n = int.Parse(things[2]);
                    l = int.Parse(things[3]);
                    /*
                     * Run Data Format:
                     * type d n lval nt maxrecl nmaxrecl dt maxprime dtper(us)
                     */
                    if (labels.Contains(templab))
                    {

                        i0 = labels.IndexOf(templab);
                        Console.WriteLine("DEBUG0: " + aaa + " " + bbb + " " + i0);
                        thing = (LabeledThing)labeled[i0];
                        thing.AddRun(nt, maxrecl, nmaxrecl, dt, maxprime);
                        labeled[i0] = thing;
                    }
                    else
                    {

                        i0 = labels.Count;
                        labels.Add(templab);
                        thing = new LabeledThing();
                        Console.WriteLine("DEBUG1: " + aaa + " " + bbb + " " + i0);
                        thing.Setup(type, d, n, l);
                        dong = thing.GetScore();
                        scores0.Add(thing.GetScore());
                        scores1.Add(thing.GetScore());
                        thing.AddRun(nt, maxrecl, nmaxrecl, dt, maxprime);
                        labeled.Add(thing);
                    }
                }
            }
            nlabed = labels.Count;


            scores1.Sort();
            Console.WriteLine(nlabed);
            lines2 = new string[nlabed + 1];
            /*
             * Format:
             * type d n l nt maxrecl nmaxrecl dt maxprime dtper(us) nruns
             */
            path3 = @"C:\rubiks\standard\cstuff\data\data0.csv";
            lines2[0] = "type,d,n,l,nt,maxrecl,nmaxrecl,dt,maxprime,dtper(us),nruns";
            for (int aaa = 0; aaa < nlabed; aaa++)
            {

                dong = (long)scores1[aaa];
                i1 = scores0.IndexOf(dong);
                thing = (LabeledThing)labeled[i1];
                lines2[aaa + 1] = thing.MakeOutputString();
            }
            System.IO.File.WriteAllLines(path3, lines2);
        }

    }
    class LabeledThing
    {
        private int type, d, n, l, nruns, nt, nmaxrecl, maxprime, state0;
        private long score, dt, dtper;
        private string milk;
        private BigInteger maxrecl;
        public void Setup(int typein, int din, int nin, int lin)
        {
            type = typein;
            d = din;
            n = nin;
            l = lin;
            /*
             * Score is a way to sort these by type,d,n,l by sorting them from least score to greatest.
             */
            score = ((long)l * 1000) + ((long)n * (1000 * 100)) + ((long)d * (10 * 1000 * 100)) + ((long)type * (5 * 10 * 1000 * 100));
            Console.WriteLine("DONG: " + score);
            nruns = 0;
            maxprime = 1;
            nmaxrecl = 0;
            nt = 0;
            dt = 0;
            maxrecl = 0;
        }

        public void AddRun(int ntin, BigInteger maxreclin, int nmaxreclin, long dtin, int maxprimein)
        {
            nt += ntin;
            dt += dtin;
            state0 = -1;
            nruns++;
            if (maxreclin > maxrecl)
            {
                state0 = 1;
            }
            else if (maxreclin == maxrecl)
            {
                state0 = 0;
            }
            if (state0 == 0)
            {
                nmaxrecl += nmaxreclin;

            }
            else if (state0 == 1)
            {
                nmaxrecl = nmaxreclin;
                maxrecl = maxreclin;
            }
            if (maxprimein > maxprime)
            {
                maxprime = maxprimein;
            }
        }
        public string MakeOutputString()
        {
            /*
             * Format:
             * type d n l nt maxrecl nmaxrecl dt maxprime dtper(us) nruns
             */
            dtper = dt * 1000;
            dtper /= nt;
            milk = type.ToString() + "," + d.ToString() + "," + n.ToString() + "," + l.ToString() + "," + nt.ToString() + "," + maxrecl.ToString() + "," + nmaxrecl.ToString() + "," + dt.ToString() + "," + maxprime.ToString() + "," + dtper.ToString() + "," + nruns;
            return milk;
        }
        public long GetScore()
        {
            return score;
        }
    }
    class RunStuff1
    {
        private long st, dt, et, dt2;
        /*
         * This does multiple runs and saves the results as a file.
         * 
         */
        private string line, line2;
        private string[] things, lines, lines2;
        private int[] bbins2, bbins10;
        private bool usedist;
        private int type, d, n, lmin, lmax, nt, fcount, nlv, nmin, nmax, cunt, nb2, nb10, maxp, nb, nl2;
        private int gcount;
        private RunStuff0 rss0;
        private long tottake, dtper;
        private ArrayList binos, countos, lineos;

        private string path, path2, path3, path4;
        public void DoIt3(String linein, int[] primes2in)
        {
            line = linein;
            things = line.Split(" ");
            type = int.Parse(things[0]);
            d = int.Parse(things[1]);
            nmin = int.Parse(things[2]);
            nmax = int.Parse(things[3]);
            lmin = int.Parse(things[4]);
            lmax = int.Parse(things[5]);
            usedist = bool.Parse(things[7]);
            dt2 = long.Parse(things[6]);
            rss0 = new RunStuff0();
            path2 = @"C:\rubiks\standard\cstuff\runs\";
            fcount = Directory.GetFiles(path2, "*", SearchOption.TopDirectoryOnly).Length;
            path = path2 + "run_" + fcount.ToString() + ".dat";
            nlv = ((lmax + 1) - lmin) * ((nmax + 1) - nmin);
            tottake = nlv * dt2;


            Console.WriteLine("TOTALTAKE: " + tottake);
            cunt = 1;
            path4 = @"C:\rubiks\standard\cstuff\dists\";
            gcount = Directory.GetFiles(path4, "*", SearchOption.TopDirectoryOnly).Length;
            lines = new string[(nlv * 2) + 1];
            if (usedist)
            {
                lines[0] = "true " + gcount;
                lineos = new ArrayList();
                line = "run = " + fcount;
                lineos.Add(line);
                path3 = path4 + "dist_" + gcount.ToString() + ".dat";

            }
            else
            {
                lines[0] = "false";
            }

            for (int zzz = nmin; zzz < nmax + 1; zzz++)
            {
                n = zzz;
                for (int aaa = lmin; aaa < lmax + 1; aaa++)
                {


                    rss0.DoStuff(type, d, n, primes2in, aaa, dt2, usedist);
                    if (usedist)
                    {
                        binos = rss0.GetBinos2();
                        countos = rss0.GetCountos2();
                        nb = rss0.GetNB();
                        line = type.ToString() + " " + d.ToString() + " " + n.ToString() + " " + aaa.ToString() + " " + nb.ToString();
                        lineos.Add(line);
                        for (int ttt = 0; ttt < nb; ttt++)
                        {
                            line = binos[ttt].ToString() + " " + countos[ttt].ToString();
                            lineos.Add(line);
                        }
                    }
                    maxp = rss0.GetMaxPrime();
                    Console.WriteLine("PARAMS: " + type + " " + d + " " + n + " " + aaa + " " + dt2 + " " + rss0.GetMax().ToString());
                    dtper = dt2 * 1000;
                    if (rss0.GetNT() != 0)
                    {
                        dtper /= rss0.GetNT();
                    }
                    /*
                     * Run Data Format:
                     * type d n lval nt maxrecl nmaxrecl dt maxprime dtper(us)
                     */
                    line = type.ToString() + " " + d.ToString() + " " + n.ToString() + " " + aaa.ToString() + " " + rss0.GetNT().ToString() + " " + rss0.GetMax().ToString() + " " + rss0.GetNMax().ToString() + " " + dt2.ToString() + " " + maxp + " " + dtper;
                    lines[cunt] = line;

                    cunt++;
                    line2 = "";
                    bbins2 = rss0.GetBBins2();
                    bbins10 = rss0.GetBBins10();
                    Console.WriteLine("BBINS10L: " + bbins10.Length);

                    line2 = "";
                    for (int bbb = 0; bbb < bbins10.Length; bbb++)
                    {
                        line2 = line2 + bbins10[bbb] + " ";
                    }
                    lines[cunt] = line2;
                    cunt++;

                }
            }
            System.IO.File.WriteAllLines(path, lines);
            if (usedist)
            {
                nl2 = lineos.Count;
                lines2 = new string[nl2];
                for (int bbb = 0; bbb < nl2; bbb++)
                {
                    lines2[bbb] = (string)lineos[bbb];
                }
                System.IO.File.WriteAllLines(path3, lines2);
            }
        }
        public void DoIt(int[] primes2in)
        {
            /*
             * Input format: type d nmin nmax lmin lmax dt dist
             */

            Console.WriteLine("Enter type d nmin nmax lmin lmax dt dist below:");
            line = Console.ReadLine();
            things = line.Split(" ");
            type = int.Parse(things[0]);
            d = int.Parse(things[1]);
            nmin = int.Parse(things[2]);
            nmax = int.Parse(things[3]);
            lmin = int.Parse(things[4]);
            lmax = int.Parse(things[5]);
            usedist = bool.Parse(things[7]);
            dt2 = long.Parse(things[6]);
            rss0 = new RunStuff0();
            path2 = @"C:\rubiks\standard\cstuff\runs\";
            fcount = Directory.GetFiles(path2, "*", SearchOption.TopDirectoryOnly).Length;
            path = path2 + "run_" + fcount.ToString() + ".dat";
            nlv = ((lmax + 1) - lmin) * ((nmax + 1) - nmin);
            tottake = nlv * dt2;
            Console.WriteLine("TOTALTAKE: " + tottake);
            cunt = 1;
            path4 = @"C:\rubiks\standard\cstuff\dists\";
            gcount = Directory.GetFiles(path4, "*", SearchOption.TopDirectoryOnly).Length;
            lines = new string[(nlv * 2) + 1];
            if (usedist)
            {
                lines[0] = "true " + gcount;
                lineos = new ArrayList();
                line = "run = " + fcount;
                lineos.Add(line);
                path3 = path4 + "dist_" + gcount.ToString() + ".dat";

            }
            else
            {
                lines[0] = "false";
            }

            for (int zzz = nmin; zzz < nmax + 1; zzz++)
            {
                n = zzz;
                for (int aaa = lmin; aaa < lmax + 1; aaa++)
                {


                    rss0.DoStuff(type, d, n, primes2in, aaa, dt2, usedist);
                    if (usedist)
                    {
                        binos = rss0.GetBinos2();
                        countos = rss0.GetCountos2();
                        nb = rss0.GetNB();
                        line = type.ToString() + " " + d.ToString() + " " + n.ToString() + " " + aaa.ToString() + " " + nb.ToString();
                        lineos.Add(line);
                        for (int ttt = 0; ttt < nb; ttt++)
                        {
                            line = binos[ttt].ToString() + " " + countos[ttt].ToString();
                            lineos.Add(line);
                        }
                    }
                    maxp = rss0.GetMaxPrime();
                    Console.WriteLine("PARAMS: " + type + " " + d + " " + n + " " + aaa + " " + dt2 + " " + rss0.GetMax().ToString());
                    dtper = dt2 * 1000;
                    dtper /= rss0.GetNT();
                    /*
                     * Run Data Format:
                     * type d n lval nt maxrecl nmaxrecl dt maxprime dtper(us)
                     */
                    line = type.ToString() + " " + d.ToString() + " " + n.ToString() + " " + aaa.ToString() + " " + rss0.GetNT().ToString() + " " + rss0.GetMax().ToString() + " " + rss0.GetNMax().ToString() + " " + dt2.ToString() + " " + maxp + " " + dtper;
                    lines[cunt] = line;

                    cunt++;
                    line2 = "";
                    bbins2 = rss0.GetBBins2();
                    bbins10 = rss0.GetBBins10();
                    Console.WriteLine("BBINS10L: " + bbins10.Length);

                    line2 = "";
                    for (int bbb = 0; bbb < bbins10.Length; bbb++)
                    {
                        line2 = line2 + bbins10[bbb] + " ";
                    }
                    lines[cunt] = line2;
                    cunt++;

                }
            }
            System.IO.File.WriteAllLines(path, lines);
            if (usedist)
            {
                nl2 = lineos.Count;
                lines2 = new string[nl2];
                for (int bbb = 0; bbb < nl2; bbb++)
                {
                    lines2[bbb] = (string)lineos[bbb];
                }
                System.IO.File.WriteAllLines(path3, lines2);
            }
        }
    }
    class RunStuff0
    {
        private int[] bins2, bins10, bbins2, bbins10;
        private PosslistStuff plz;
        private HardWay hard;
        private MovesetStuff mss;
        private bool usedist, b0, tf;
        private MoveItSingle song;
        private int type, n, d, l, nt, l0, nofmax, maxp, b2, b10, nb2, nb10, i0, i1, c0, c1, nb, nfalse;
        private int[] primes, checko;
        private PossProcessor proc;
        private BigInteger biggy, max;
        private BigInteger[] binz2, binz10;
        private bool[] tfvs;
        private ArrayList binos, binos2;
        private ArrayList countos, countos2;
        private long st, et, dt, dt2, dt3, st3;
        /*
         * This puts everything for finding the max recurrence length for a given set of values up to l.
         * 
         */
        public BigInteger GetMax()
        {
            return max;
        }
        public int GetNMax()
        {
            return nofmax;
        }
        public int GetNT()
        {
            return nt;
        }
        public void DoStuff2(int typein, int din, int nin, int[] primes2in, int lin, long dtin)
        {
            primes = primes2in;
            type = typein;
            d = din;
            n = nin;
            l = lin;
            st = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            dt2 = dtin;
            plz = new PosslistStuff();
            plz.LoadPossList(type, d, n);
            mss = new MovesetStuff();
            mss.LoadMovesetStuff(type, d, n);
            song = new MoveItSingle();
            nofmax = 0;
            song.StartIt(primes, mss.GetMoveSet(), mss.GetNF(), plz.GetPossList(), plz.GetNR(), plz.GetNC(), mss.GetNMovez(), type);
            song.StartIt2(l);

            proc = new PossProcessor();
            proc.StartIt(plz.GetPossList(), plz.GetNR(), plz.GetNC(), primes);
            nt = 0;
            dt = 0;
            checko = plz.GetChecko();
            song.StartIt3(checko);
            nfalse = 0;
            while (dt < dt2)
            {
                nt++;

                song.DoSingleRun2();
                tf = song.GetTF();
                if (tf)
                {
                    nfalse++;
                }
                et = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                dt = et - st;
            }
            Console.WriteLine("NFALSE: " + nfalse + " " + nt);
        }
        public void DoStuff(int typein, int din, int nin, int[] primes2in, int lin, long dtin, bool usedistin)
        {

            /*
             * Bins2:
             * [2^0 - 2^1)
             * [2^1 - 2^2)
             * ....
             * 
             * Bins10:
             * [1 - 10)
             * [10 - 100)
             * [100 - 1000)
             *....
             */
            usedist = usedistin;
            if (usedist)
            {
                binos = new ArrayList();
                /*
                 * Binos stores the Recls, Countos stores the number of each
                 */
                countos = new ArrayList();
            }
            binz2 = new BigInteger[450];
            binz10 = new BigInteger[100];
            bins2 = new int[450];
            bins10 = new int[100];
            binz2[0] = 2;
            binz10[0] = 10;
            for (int ajk = 0; ajk < 450; ajk++)
            {
                if (ajk < 100)
                {
                    if (ajk != 0)
                    {
                        binz10[ajk] = binz10[ajk - 1] * 10;
                    }
                    bins10[ajk] = 0;
                }
                if (ajk != 0)
                {
                    binz2[ajk] = binz2[ajk] * 2;
                }
                bins2[ajk] = 0;
            }
            primes = primes2in;
            type = typein;
            d = din;
            n = nin;
            l = lin;
            st = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            dt2 = dtin;
            plz = new PosslistStuff();
            plz.LoadPossList(type, d, n);
            mss = new MovesetStuff();
            mss.LoadMovesetStuff(type, d, n);
            song = new MoveItSingle();
            nofmax = 0;
            song.StartIt(primes, mss.GetMoveSet(), mss.GetNF(), plz.GetPossList(), plz.GetNR(), plz.GetNC(), mss.GetNMovez(), type);
            song.StartIt2(l);
            proc = new PossProcessor();
            proc.StartIt(plz.GetPossList(), plz.GetNR(), plz.GetNC(), primes);

            max = 1;
            nt = 0;
            dt = 0;
            maxp = 1;
            while (dt < dt2)
            {
                nt++;


                song.DoSingleRun();

                if (false)
                {
                    Console.WriteLine("DEBUG 0 : " + dt);
                }
                tfvs = song.GetTFVS();
                if (false)
                {
                    Console.Write("\t");
                    l0 = tfvs.Length;
                    for (int jii = 0; jii < l0; jii++)
                    {
                        Console.Write(tfvs[jii] + " ");
                    }
                    Console.WriteLine();
                }


                proc.LoadTFVS(tfvs);
                biggy = proc.GetBiggy();
                if (usedist)
                {
                    b0 = binos.Contains(biggy);
                    if (b0)
                    {
                        i0 = binos.IndexOf(biggy);
                        c0 = (int)countos[i0];
                        c0++;
                        countos[i0] = c0;

                    }
                    else
                    {
                        binos.Add(biggy);
                        countos.Add(1);
                    }
                }
                if (maxp < proc.GetMaxPrime())
                {
                    maxp = proc.GetMaxPrime();
                }
                for (int ajk = 0; ajk < 450; ajk++)
                {
                    if (biggy < binz2[ajk])
                    {
                        b2 = ajk;
                        ajk = 450;
                    }
                }
                for (int ajk = 0; ajk < 100; ajk++)
                {
                    if (biggy < binz10[ajk])
                    {
                        b10 = ajk;
                        ajk = 100;
                    }
                }
                bins2[b2]++;
                bins10[b10]++;

                if (false)
                {
                    Console.WriteLine("DEBUG 1 : " + dt);
                }
                if (biggy == max)
                {
                    nofmax++;
                }
                if (biggy > max)
                {
                    nofmax = 1;
                    max = biggy;
                }
                if (false)
                {
                    Console.WriteLine(biggy);
                }
                et = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                dt = et - st;
            }
            if (true)
            {
                Console.WriteLine("MAX: " + max + " Number of Max: " + nofmax);
            }
            if (usedist)
            {
                binos2 = new ArrayList();
                nb = binos.Count;
                for (int ajk = 0; ajk < nb; ajk++)
                {
                    binos2.Add(binos[ajk]);
                }
                binos2.Sort();
                countos2 = new ArrayList();
                for (int ajk = 0; ajk < nb; ajk++)
                {
                    i1 = binos.IndexOf(binos2[ajk]);
                    countos2.Add(countos[i1]);
                }

            }

            for (int ajk = 449; ajk > -1; ajk--)
            {
                if (bins2[ajk] != 0)
                {
                    nb2 = ajk + 1;
                    ajk = -1;
                }
            }
            for (int ajk = 99; ajk > -1; ajk--)
            {
                if (bins10[ajk] != 0)
                {
                    nb10 = ajk + 1;
                    ajk = -1;
                }
            }
            bbins2 = new int[nb2];
            bbins10 = new int[nb10];
            for (int ajk = 0; ajk < nb2; ajk++)
            {
                bbins2[ajk] = bins2[ajk];
            }
            for (int ajk = 0; ajk < nb10; ajk++)
            {
                bbins10[ajk] = bins10[ajk];
            }
        }
        public int GetMaxPrime()
        {
            return maxp;
        }
        public ArrayList GetBinos2()
        {
            return binos2;
        }
        public ArrayList GetCountos2()
        {
            return countos2;
        }
        public int GetNB()
        {
            return nb;
        }
        public int[] GetBBins2()
        {
            return bbins2;
        }
        public int[] GetBBins10()
        {
            return bbins10;
        }
    }
    class PossProcessor
    {
        private int[,] posslist, seto;
        private int nr, nc, maxprime, nproo;
        private int[] maxeach;
        private int[] notmaxinds, bigbad, primes;
        private int[] powtally;
        private BigInteger biggy;
        public void StartIt(int[,] posslistin, int nrin, int ncin, int[] primes2in)
        {
            primes = primes2in;
            nproo = primes.Length;
            posslist = posslistin;
            nr = nrin;
            nc = ncin;
            maxeach = new int[nc];
            for (int aja = 0; aja < nc; aja++)
            {
                maxeach[aja] = 0;
            }
            notmaxinds = new int[nr];
            powtally = new int[nc];
            bigbad = new int[nc];
            Analyze0();
        }
        public void Analyze0()
        {
            for (int aja = 0; aja < nr; aja++)
            {
                for (int bjb = 0; bjb < nc; bjb++)
                {
                    if (posslist[aja, bjb] > maxeach[bjb])
                    {
                        maxeach[bjb] = posslist[aja, bjb];
                        bigbad[bjb] = posslist[aja, bjb];

                        /*
                         * We start with the maximum prime powers and then we lower them based on falses.
                         */
                    }
                }
            }

            /*
             * We will then use the notmaxinds to find which of these values we will look at for each row.
             */

            for (int aja = 0; aja < nr; aja++)
            {
                for (int bjb = 0; bjb < nc; bjb++)
                {
                    if (maxeach[bjb] > posslist[aja, bjb])
                    {
                        notmaxinds[aja] = bjb;
                        bjb = nc;
                    }
                }
            }
        }
        public void LoadTFVS(bool[] tfvsin)
        {
            biggy = 1;
            for (int bjb = 0; bjb < nc; bjb++)
            {
                powtally[bjb] = bigbad[bjb];
            }
            for (int aja = 0; aja < nr; aja++)
            {
                if (!tfvsin[aja])
                {
                    powtally[notmaxinds[aja]]--;
                }
            }
            for (int bjb = 0; bjb < nc; bjb++)
            {
                if (powtally[bjb] == 0)
                {

                }
                else
                {
                    for (int cjc = 0; cjc < powtally[bjb]; cjc++)
                    {
                        biggy *= primes[bjb];
                    }
                }
            }
        }
        public int GetMaxPrime()
        {
            maxprime = 1;
            for (int ddd = 0; ddd < powtally.Length; ddd++)
            {
                if (powtally[ddd] != 0)
                {
                    maxprime = primes[ddd];
                }
            }
            return maxprime;
        }
        public BigInteger GetBiggy()
        {
            return biggy;
        }
    }

    class PosslistStuff
    {
        private string patho, nin2, din2;
        private string[] texto;
        private string[] things;
        private int[,] posslis;
        private int[] checko;
        private int nc, nr;
        public void LoadPossList(int typein, int din, int nin)
        {
            nin2 = nin.ToString();
            din2 = din.ToString();
            if (typein == 0)
            {
                patho = @"C:\rubiks\standard\posslist\" + din2 + "d" + nin2 + "poss.dat";

            }
            else if (typein == 2)
            {
                patho = @"C:\rubiks\standard\posslist\cubeoposs.dat";
            }
            else if (typein == 1)
            {
                patho = @"C:\rubiks\standard\posslist\megaposs.dat";
            }
            else
            {
                patho = @"C:\rubiks\standard\posslist\pyrposs.dat";
            }

            texto = System.IO.File.ReadAllLines(patho);

            things = texto[0].Split(" ");
            nr = int.Parse(things[0]);
            nc = int.Parse(things[1]);
            posslis = new int[nr, nc];
            checko = new int[nc];
            if (false)
            {
                Console.WriteLine();
            }
            for (int ai = 0; ai < nr; ai++)
            {
                things = texto[ai + 1].Split(" ");
                if (false)
                {
                    Console.Write("\t");
                }
                for (int bi = 0; bi < nc; bi++)
                {
                    if (ai == 0)
                    {
                        checko[bi] = int.Parse(things[bi]);
                    }
                    posslis[ai, bi] = int.Parse(things[bi]);
                    if (false)
                    {
                        Console.Write(posslis[ai, bi] + " ");
                    }
                }
                if (ai == 0)
                {
                    checko[0]++;
                }
                if (false)
                {
                    Console.WriteLine();
                }

            }
        }
        public int[] GetChecko()
        {
            return checko;
        }
        public int GetNR()
        {
            return nr;
        }
        public int[,] GetPossList()
        {
            return posslis;
        }
        public int GetNC()
        {
            return nc;
        }
    }
    class MovesetStuff
    {
        private int n, d, type, nmovestot, n0, n1, nf, n2;
        private int[,] moveset;
        private string patho, nin2, din2, typein2;
        private string[] texto;
        private string line;
        private string[] things;
        public void LoadMovesetStuff(int typein, int din, int nin)
        {
            n = nin;
            d = din;
            type = typein;
            nin2 = nin.ToString();
            din2 = din.ToString();
            typein2 = typein.ToString();
            patho = @"C:\rubiks\standard\movesets\moveset_" + typein2 + "_" + din2 + "_" + nin2 + ".dat";
            texto = System.IO.File.ReadAllLines(patho);
            things = texto[0].Split(" ");
            nmovestot = int.Parse(things[0]);
            nf = int.Parse(things[1]);
            moveset = new int[nmovestot, nf];
            for (int ai = 0; ai < nmovestot; ai++)
            {
                things = texto[ai + 1].Split(" ");
                n0 = things.Length;
                for (int bi = 0; bi < nf; bi++)
                {
                    moveset[ai, bi] = bi;
                }
                for (int bi = 0; bi < n0 - 1; bi += 2)
                {


                    n1 = int.Parse(things[bi]);

                    n2 = int.Parse(things[bi + 1]);
                    moveset[ai, n1] = n2;
                }
                if (false)
                {
                    Console.WriteLine("");
                    Console.Write("\t");
                    for (int bi = 0; bi < nf; bi++)
                    {
                        Console.Write(moveset[ai, bi] + " ");
                    }
                }
            }
        }
        public int GetNF()
        {
            return nf;
        }
        public int GetNMovez()
        {
            return nmovestot;
        }
        public int[,] GetMoveSet()
        {
            return moveset;
        }
    }
    class ConstructRecL
    {
        private int v0, v1, v2;
        private BigInteger val0, val1, val2;
        private int[] primes;
        private int nc, nr;
        private bool[] tfvs;
        private int[,] posslist;
        public void StartIt(int[] primes2in, int ncin, int nrin, int[,] posslistin)
        {
            nc = ncin;
            nr = nrin;
            val0 = 1;
            primes = primes2in;
            posslist = posslistin;
        }
        public void FindPowsOfEach(bool[] tfvsin)
        {
            tfvs = tfvsin;
            /*
             *
             */
        }


    }
    class MoveItSingle
    {
        /*
         * This is to get the map for the sequence itself and then do MoveItFromSeq.
         */
        private int[] primes2, map, map2, maporig, checko;
        private int[,] moveset, posslist;
        private int nf, nr, nc, nmovez, l, rmove, rdir, type, checkol;
        private MoveItFromSeq moon;
        private SMap cube0;
        private bool tf;
        private bool[] tfvs;
        private int[,] moveseq;
        private BigInteger val0, val1;
        private System.Random random;
        private long st, dt, et;
        public void StartIt(int[] primes2in, int[,] movesetin, int nfin, int[,] posslistin, int nrin, int ncin, int nmovezin, int typein)
        {
            moveset = movesetin;
            nmovez = nmovezin;
            type = typein;
            random = new System.Random();
            primes2 = primes2in;
            nf = nfin;
            nr = nrin;
            nc = ncin;
            posslist = posslistin;
            map = new int[nf];
            map2 = new int[nf];
            moon = new MoveItFromSeq();
            moon.StartIt(primes2, posslist, nf, nr, nc);
            cube0 = new SMap();
            cube0.StartIt(nf);
        }
        public void StartIt3(int[] checkoin)
        {
            checko = checkoin;
            checkol = checko.Length;
        }
        /*
         * StartIt deals with the stuff independent of length, so we set that first, then we go to 
         * StartIt2 to get our length of our sequence set.
         * StartIt3 to setup the Checking Thing.
         * DoSingleRun2 checks once
         * DoSingleRun does one trial.
         */
        public void StartIt2(int lin)
        {
            l = lin;
            moveseq = new int[l, 2];
        }
        public void DoSingleRun2()
        {
            cube0.ResetCubes();
            map = cube0.GetCube0();
            for (int ae = 0; ae < l; ae++)
            {
                rmove = random.Next(0, nmovez);
                rdir = random.Next(0, 2);
                moveseq[ae, 0] = rmove;
                moveseq[ae, 1] = rdir;
                for (int af = 0; af < nf; af++)
                {
                    map2[af] = moveset[rmove, af];
                }
                cube0.SetCMap(map2);
                cube0.Mapo();
                if (rdir == 1)
                {
                    if (type == 3)
                    {
                        cube0.Mapo();
                    }
                    else if (type == 1)
                    {
                        cube0.Mapo();
                        cube0.Mapo();
                        cube0.Mapo();
                    }
                    else
                    {
                        cube0.Mapo();
                        cube0.Mapo();
                    }
                }
            }
            map = cube0.GetCube0();
            maporig = cube0.GetCube0();
            moon.Start2(map);
            moon.DoSingleLevel(checko);
            tf = moon.GetStillGoing();
        }
        public bool GetTF()
        {
            return tf;
        }
        public void DoSingleRun()
        {


            cube0.ResetCubes();
            map = cube0.GetCube0();
            for (int ae = 0; ae < l; ae++)
            {
                rmove = random.Next(0, nmovez);
                rdir = random.Next(0, 2);
                moveseq[ae, 0] = rmove;
                moveseq[ae, 1] = rdir;
                for (int af = 0; af < nf; af++)
                {
                    map2[af] = moveset[rmove, af];
                }
                cube0.SetCMap(map2);
                cube0.Mapo();
                if (rdir == 1)
                {
                    if (type == 3)
                    {
                        cube0.Mapo();
                    }
                    else if (type == 1)
                    {
                        cube0.Mapo();
                        cube0.Mapo();
                        cube0.Mapo();
                    }
                    else
                    {
                        cube0.Mapo();
                        cube0.Mapo();
                    }
                }
            }
            map = cube0.GetCube0();
            maporig = cube0.GetCube0();
            moon.StartWithMap(map);
            tfvs = moon.GetTFVS();
        }
        public int[] GetMapOrig()
        {
            return maporig;
        }
        public void makeIntoBigInteger()
        {

        }
        public bool[] GetTFVS()
        {
            return tfvs;
        }
    }
    class MoveItFromSeq
    {
        /*
         * This is to go from the map after doing the sequence once to getting the true false array.
         */
        private int[] primes2, map, map2, map3, powz;
        private int[,] posslist;
        private int nf, nr, nc;
        private bool stillgoing;
        private SMap smop;
        private bool[] tfvs;

        public void StartIt(int[] primes2in, int[,] posslistin, int nfin, int nrin, int ncin)
        {
            primes2 = primes2in;
            posslist = posslistin;
            nf = nfin;
            nr = nrin;
            nc = ncin;
            powz = new int[nc];
            smop = new SMap();
            smop.StartIt(nf);
            smop.ResetCubes();
            tfvs = new bool[nr];
        }
        public void StartWithMap(int[] mapin)
        {
            map = mapin;
            smop.ResetCubes();
            smop.SetCMap(map);
            stillgoing = true;
            for (int aaq = 0; aaq < nr; aaq++)
            {
                for (int fq = 0; fq < nc; fq++)
                {
                    powz[fq] = posslist[aaq, fq];
                }
                DoSingleLevel(powz);
                tfvs[aaq] = stillgoing;
            }
        }
        public void Start2(int[] mapin)
        {
            map = mapin;

        }
        public bool[] GetTFVS()
        {
            return tfvs;
        }
        public bool GetStillGoing()
        {
            return stillgoing;
        }
        public void DoSingleLevel(int[] powsin)
        {
            map2 = new int[nf];
            map3 = new int[nf];
            smop.ResetCubes();
            for (int gttt = 0; gttt < nf; gttt++)
            {
                map2[gttt] = map[gttt];
            }
            stillgoing = true;
            for (int aq = 0; aq < nc; aq++)
            {
                if (powsin[aq] == 0)
                {

                }
                else
                {
                    for (int bq = 0; bq < powsin[aq]; bq++)
                    {
                        if (stillgoing)
                        {
                            smop.ResetCubes();
                            smop.SetCMap(map2);
                            for (int cq = 0; cq < primes2[aq]; cq++)
                            {
                                smop.Mapo();
                            }
                            map3 = smop.GetCube0();
                            for (int dq = 0; dq < nf; dq++)
                            {
                                map2[dq] = map3[dq];
                            }
                            if (smop.GetOrig())
                            {
                                stillgoing = false;
                            }
                        }
                    }
                }
            }
        }
    }
    class SMap
    {
        private int nf;
        private int[] cube0, cube1, map;
        private bool orig;
        public void StartIt(int nfin)
        {
            nf = nfin;
            cube0 = new int[nf];
            cube1 = new int[nf];
            map = new int[nf];

        }
        public void ResetCubes()
        {
            for (int gt = 0; gt < nf; gt++)
            {
                cube0[gt] = gt;
                cube1[gt] = gt;

            }

        }
        public void SetCMap(int[] mapin)
        {
            map = mapin;
        }

        public void Mapo()
        {
            for (int gt = 0; gt < nf; gt++)
            {
                cube1[map[gt]] = cube0[gt];
            }
            for (int gt = 0; gt < nf; gt++)
            {
                cube0[gt] = cube1[gt];
            }
        }
        public int[] GetCube0()
        {
            return cube0;
        }
        public void printCube0()
        {
            Console.Write("\t");
            for (int gt = 0; gt < nf; gt++)
            {
                Console.Write(cube0[gt] + " ");

            }
            Console.WriteLine();
        }
        public bool GetOrig()
        {
            orig = true;
            for (int gt = 0; gt < nf; gt++)
            {
                if (cube0[gt] != gt)
                {
                    orig = false;
                    gt = nf;
                }
            }
            return orig;
        }
    }

}
