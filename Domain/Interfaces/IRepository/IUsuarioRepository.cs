﻿using Domain.Entity;

namespace Domain.Interfaces.IRepository
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Usuario obterPorNome(string nome);
    }
}
