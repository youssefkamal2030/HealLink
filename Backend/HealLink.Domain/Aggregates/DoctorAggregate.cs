using System;
using System.Collections.Generic;
using HealLink.Domain.Entities;
using HealLink.Domain.ValueObjects;

namespace HealLink.Domain.Aggregates
{
    public class DoctorAggregate
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

        public void AcceptPatientRequest(Guid connectionId)
        {
            var connection = _connections.Find(c => c.Id == connectionId);
            if (connection == null) throw new InvalidOperationException("Connection not found.");
            connection.Accept();
        }

        public void UpdateClinicAddress(Address newAddress)
        {
            ClinicAddress = newAddress ?? throw new ArgumentNullException(nameof(newAddress));
        }
    }
} 