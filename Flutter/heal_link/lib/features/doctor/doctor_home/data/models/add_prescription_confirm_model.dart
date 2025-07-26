import 'package:flutter/cupertino.dart';
import 'package:heal_link/features/doctor/doctor_home/data/models/add_prescription_model.dart';


class AddPrescriptionConfirmModel {
  final TextEditingController dateController;
  final TextEditingController ageController;
  final TextEditingController patientName;
  final List<AddPrescriptionModel> medicineList;

  AddPrescriptionConfirmModel(
    this.ageController,
    this.patientName,
    this.medicineList, {
    required this.dateController,
  });
}
