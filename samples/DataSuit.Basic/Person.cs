namespace DataSuit.Basic
{
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Note { get; set; }
        public double CreditNote { get; set; }


        public override string ToString()
        {
            return $"FirstName:{FirstName}, LastName:{LastName}, Age:{Age}";
        }

        public string NoteAndCreditNote()
        {
            return $"Personal Note:{Note},CreditNote:{CreditNote}";
        }
    }
}