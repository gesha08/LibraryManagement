using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Data.Entities
{
    public class BookReader
    {
        public int BookId { get; set; }

        [ForeignKey("BookId")]
        public Book Book { get; set; }

        public int ReaderId { get; set; }

        [ForeignKey("ReaderId")]
        public Reader Reader { get; set; }

        public DateTime BorrowedDate { get; set; }

        public DateTime? ReturnedDate { get; set; }
    }
}