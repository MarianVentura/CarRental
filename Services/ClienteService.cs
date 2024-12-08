﻿using Microsoft.EntityFrameworkCore;
using CarRental.Data;
using CarRental.Models;
using CarRental.Extensors;
using BlazorBootstrap;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace CarRental.Services
{
    public class ClienteService
    {
        private readonly IDbContextFactory<Contexto> _dbFactory;
        private readonly ToastService _toastService;

        public ClienteService(IDbContextFactory<Contexto> dbFactory, ToastService toastService)
        {
            _dbFactory = dbFactory;
            _toastService = toastService;
        }

        // Obtener todos los clientes
        public async Task<List<Cliente>> ObtenerTodosLosClientes()
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            return await contexto.Clientes
                .AsNoTracking()
                .ToListAsync();
        }

        // Obtener un cliente por ID
        public async Task<Cliente?> ObtenerClientePorId(int id)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            var cliente = await contexto.Clientes
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.ClienteId == id);

            if (cliente == null)
            {
                _toastService.ShowError($"El cliente con ID {id} no fue encontrado.");
                return null;
            }

            return cliente;
        }

        // Agregar un nuevo cliente
        public async Task<bool> AgregarCliente(Cliente cliente)
        {
            if (cliente == null)
            {
                _toastService.ShowError("El cliente no puede ser nulo.");
                return false;
            }

            // Validar propiedades del cliente usando el modelo
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(cliente);

            if (!Validator.TryValidateObject(cliente, validationContext, validationResults, true))
            {
                foreach (var error in validationResults)
                {
                    _toastService.ShowError(error.ErrorMessage);
                }
                return false;
            }

            await using var contexto = await _dbFactory.CreateDbContextAsync();
            contexto.Clientes.Add(cliente);
            await contexto.SaveChangesAsync();

            _toastService.ShowSuccess("Cliente agregado exitosamente.");
            return true;
        }

        // Actualizar un cliente existente
        public async Task<bool> ActualizarCliente(Cliente cliente)
        {
            if (cliente == null)
            {
                _toastService.ShowError("El cliente no puede ser nulo.");
                return false;
            }

            // Validar propiedades del cliente usando el modelo
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(cliente);

            if (!Validator.TryValidateObject(cliente, validationContext, validationResults, true))
            {
                foreach (var error in validationResults)
                {
                    _toastService.ShowError(error.ErrorMessage);
                }
                return false;
            }

            await using var contexto = await _dbFactory.CreateDbContextAsync();
            var clienteExistente = await contexto.Clientes.FindAsync(cliente.ClienteId);

            if (clienteExistente == null)
            {
                _toastService.ShowError($"El cliente con ID {cliente.ClienteId} no fue encontrado.");
                return false;
            }

            // Actualizar las propiedades del cliente
            clienteExistente.Nombres = cliente.Nombres;
            clienteExistente.Email = cliente.Email;
            clienteExistente.Telefono = cliente.Telefono;
            clienteExistente.Direccion = cliente.Direccion;
            clienteExistente.Identificacion = cliente.Identificacion;

            contexto.Clientes.Update(clienteExistente);
            await contexto.SaveChangesAsync();

            _toastService.ShowSuccess("Cliente actualizado exitosamente.");
            return true;
        }

        // Eliminar un cliente por ID
        public async Task<bool> EliminarCliente(int clienteId)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            var cliente = await contexto.Clientes.FindAsync(clienteId);

            if (cliente == null)
            {
                _toastService.ShowError($"El cliente con ID {clienteId} no fue encontrado.");
                return false;
            }

            contexto.Clientes.Remove(cliente);
            await contexto.SaveChangesAsync();

            _toastService.ShowSuccess("Cliente eliminado exitosamente.");
            return true;
        }

        // Buscar clientes por nombre
        public async Task<List<Cliente>> BuscarClientesPorNombre(string nombre)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            return await contexto.Clientes
                .Where(c => c.Nombres.Contains(nombre))
                .AsNoTracking()
                .ToListAsync();
        }

        // Obtener clientes por tipo de identificación
        public async Task<List<Cliente>> ObtenerClientesPorTipoIdentificacion(string tipoIdentificacion)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();

            return await contexto.Clientes
                .Where(c => c.Identificacion.ToString() == tipoIdentificacion) // Convertir Identificacion a string
                .AsNoTracking()
                .ToListAsync();
        }

    }
}