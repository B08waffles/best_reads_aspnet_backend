using best_reads_backend.Models;

namespace best_reads_backend.Data
{
    public static class DbInitialiser
    {
        public static void Initialise(LibraryContext context)
        {
            if (context.Authors.Any())
            {
                return; // DB has been seeded
            }

            // Authors
            var george_orwell = new Author
            {
                Id = 1,
                FirstName = "George",
                LastName = "Orwell",
                DOB = "1950-06-25",
            };

            // Books
            var nine_teen_eighty_four = new Book
            {
                Id = 1,
                Title = "1984",
                Published = "1949",
            };

            context.SaveChanges();
        }
    }
}
