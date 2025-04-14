using System;

namespace AplikasiGaji
{
    public abstract class Karyawan
    {
        protected string Nama { get; }
        protected string IDKaryawan { get; }
        protected double GajiPokok { get; }

        protected Karyawan(string nama, string id, double gajiPokok)
        {
            Nama = nama;
            IDKaryawan = id;
            GajiPokok = gajiPokok;
        }

        public string GetNama() => Nama;
        public string GetID() => IDKaryawan;
        public double GetGajiPokok() => GajiPokok;

        public abstract double HitungTotalGaji();
    }

    public class KaryawanTetap : Karyawan
    {
        private const double TambahanTetap = 500000;

        public KaryawanTetap(string nama, string id, double gajiPokok)
            : base(nama, id, gajiPokok) { }

        public override double HitungTotalGaji()
        {
            return GetGajiPokok() + TambahanTetap;
        }
    }

    public class KaryawanKontrak : Karyawan
    {
        private const double Potongan = 200000;

        public KaryawanKontrak(string nama, string id, double gajiPokok)
            : base(nama, id, gajiPokok) { }

        public override double HitungTotalGaji()
        {
            return GetGajiPokok() - Potongan;
        }
    }

    public class Magang : Karyawan
    {
        public Magang(string nama, string id, double gajiPokok)
            : base(nama, id, gajiPokok) { }

        public override double HitungTotalGaji()
        {
            return GetGajiPokok();
        }
    }

    public class MenuGaji
    {
        public static void Mulai()
        {
            while (true)
            {
                TampilkanPilihan();
                string input = Console.ReadLine();

                if (input == "4")
                {
                    Console.WriteLine("Terima kasih, program berakhir.");
                    break;
                }

                ProsesPilihan(input);
            }
        }

        private static void TampilkanPilihan()
        {
            Console.Clear();
            Console.WriteLine("=== Aplikasi Perhitungan Gaji ===");
            Console.WriteLine("1. Tambah Karyawan Tetap");
            Console.WriteLine("2. Tambah Karyawan Kontrak");
            Console.WriteLine("3. Tambah Magang");
            Console.WriteLine("4. Keluar");
            Console.Write("Pilih opsi (1-4): ");
        }

        private static void ProsesPilihan(string pilihan)
        {
            Console.WriteLine();
            Console.Write("Nama Karyawan: ");
            string nama = Console.ReadLine();
            Console.Write("ID Karyawan: ");
            string id = Console.ReadLine();

            double gaji;
            while (true)
            {
                Console.Write("Gaji Pokok (Rp): ");
                if (double.TryParse(Console.ReadLine(), out gaji)) break;
                Console.WriteLine("Masukkan angka yang valid!");
            }

            Karyawan karyawan = BuatKaryawan(pilihan, nama, id, gaji);

            if (karyawan != null)
            {
                TampilkanGaji(karyawan);
            }
            else
            {
                Console.WriteLine("Pilihan tidak tersedia. Tekan Enter untuk kembali...");
                Console.ReadLine();
            }
        }

        private static Karyawan BuatKaryawan(string opsi, string nama, string id, double gaji)
        {
            switch (opsi)
            {
                case "1": return new KaryawanTetap(nama, id, gaji);
                case "2": return new KaryawanKontrak(nama, id, gaji);
                case "3": return new Magang(nama, id, gaji);
                default: return null;
            }
        }

        private static void TampilkanGaji(Karyawan orang)
        {
            Console.WriteLine();
            Console.WriteLine($"Data Gaji {orang.GetNama()}:");
            Console.WriteLine($"ID: {orang.GetID()}");
            Console.WriteLine($"Gaji Pokok: Rp.{orang.GetGajiPokok()}");
            Console.WriteLine($"Gaji Total: Rp.{orang.HitungTotalGaji()}");
            Console.WriteLine("\nTekan Enter untuk kembali...");
            Console.ReadLine();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            MenuGaji.Mulai();
        }
    }
}
