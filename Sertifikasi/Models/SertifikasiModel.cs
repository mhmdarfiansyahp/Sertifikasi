using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sertifikasi.Models
{
    public class SertifikasiModel
    {
        [Key]
        public int IdDetailSertifikasi { get; set; }

        public int IdSertifikasi { get; set; }

        public DateTime Tanggal { get; set; }

        public string Lembaga { get; set; }

        public string Level { get; set; }

        public int LevelKKNI { get; set; }

        public string Pdf { get; set; }


    }
}
