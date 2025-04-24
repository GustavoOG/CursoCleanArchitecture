using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Reviews
{
    public static class ReviewErrors
    {

        public static readonly Error NotElegible = new(
            "Review.Norelegible", "Este review y calificacion no es elegible por que uan no se completa"
            );
    }
}
