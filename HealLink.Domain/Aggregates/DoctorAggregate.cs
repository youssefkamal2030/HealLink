using System;
using System.Collections.Generic;
using System.Linq;
using HealLink.Domain.Base;
using HealLink.Domain.DomainEvents;
using HealLink.Domain.Entities;
using HealLink.Domain.Enums;
using HealLink.Domain.ValueObjects;

namespace HealLink.Domain.Aggregates
{
    public class DoctorAggregate : AggregateRoot
    {
        public Doctor Doctor { get; private set; }
        public Address ClinicAddress { get; private set; }
        private readonly List<DoctorPatientConnection> _connections = new();

        public IReadOnlyCollection<DoctorPatientConnection> Connections => _connections.AsReadOnly();

        public DoctorAggregate(Doctor doctor, Address clinicAddress, IEnumerable<DoctorPatientConnection> connections)
        {
            Doctor = doctor ?? throw new ArgumentNullException(nameof(doctor));
            ClinicAddress = clinicAddress ?? throw new ArgumentNullException(nameof(clinicAddress));
            if (connections != null) _connections.AddRange(connections);
        }

        // In DoctorAggregate.cs
        public void AcceptPatientRequest(Guid connectionId)
        {
            var connection = _connections.Find(c => c.Id == connectionId);
            if (connection == null) throw new InvalidOperationException("Connection not found.");
            if (connection.Status != ConnectionStatus.Pending) throw new InvalidOperationException("Not pending.");

            connection.Accept();
            Doctor.AddPatient(connection.PatientId);  // Sync _patientIds
            
            // Raise domain event
            AddDomainEvent(new ConnectionAcceptedEvent(
                connectionId,
                Doctor.Id,
                connection.PatientId,
                DateTime.UtcNow
            ));
            
            // Note: Also sync Patient's list – load PatientAggregate if needed, or raise event to handle async
        }
        
        public void RejectPatientRequest(Guid connectionId)
        {
            var connection = _connections.Find(c => c.Id == connectionId);
            if (connection == null) throw new InvalidOperationException("Connection not found.");
            if (connection.Status != ConnectionStatus.Pending) throw new InvalidOperationException("Not pending.");

            connection.Reject();
            _connections.Remove(connection); 
            Doctor.PatientConnections.Remove(connection);
            
            // Raise domain event
            AddDomainEvent(new ConnectionRejectedEvent(
                connectionId,
                Doctor.Id,
                connection.PatientId
            ));
        }

        public void UpdateClinicAddress(Address newAddress)
        {
            ClinicAddress = newAddress ?? throw new ArgumentNullException(nameof(newAddress));
        }
        public void AddConnection(DoctorPatientConnection connection)
        {
            if (connection == null) throw new ArgumentNullException(nameof(connection));
            if (_connections.Any(c => c.PatientId == connection.PatientId && c.Status != ConnectionStatus.Rejected))
                throw new InvalidOperationException("Connection already exists or pending.");
            _connections.Add(connection);
            Doctor.PatientConnections.Add(connection);  // Sync navigation
        }

    }
} 