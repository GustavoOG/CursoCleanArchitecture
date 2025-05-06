﻿using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Alquileres;
using CleanArchitecture.Domain.Alquileres.Events;
using CleanArchitecture.Domain.Users;
using CleanArchitecture.Domain.Vehiculos;

namespace CleanArchitecture.Domain.Reviews
{
    public sealed class Review : Entity<ReviewId>
    {
        private Review() { }
        private Review(
            ReviewId id,
            VehiculoId vehiculoId,
            AlquilerId alquilerId,
            UserId userId,
            Rating rating,
            Comentario comentario,
            DateTime? fechaCreacion) : base(id)
        {
            VehiculoId = vehiculoId;
            AlguilerId = alquilerId;
            UserId = userId;
            Rating = rating;
            Comentario = comentario;
            FechaCreacion = fechaCreacion;

        }
        public VehiculoId? VehiculoId { get; private set; }
        public AlquilerId? AlguilerId { get; private set; }
        public UserId? UserId { get; private set; }
        public Rating? Rating { get; private set; }
        public Comentario? Comentario { get; private set; }
        public DateTime? FechaCreacion { get; private set; }

        public static Result<Review> Create(
            Alquiler alquiler, Rating rating, Comentario comentario, DateTime fechaCreacion)
        {
            if (alquiler.Status != AlquilerStatus.Completado)
            {
                return Result.Failure<Review>(ReviewErrors.NotElegible);
            }
            var review = new Review(ReviewId.New(), alquiler.VehiculoId!, alquiler.Id!, alquiler.UserId!,
                rating, comentario, fechaCreacion);
            review.RaiseDomianEvent(new ReviewCreateDomianEvent(review.Id!));
            return review;
        }
    }
}
