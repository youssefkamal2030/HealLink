import 'package:flutter/material.dart';
import 'package:heal_link/features/doctor/doctor_home/presentation/views/widgets/add_prescription_card.dart';
import '../../../../../core/widgets/custom_app_bar_pop_widget.dart';
import '../../../../../core/widgets/custom_full_button.dart';
import '../../../../../generated/l10n.dart';
import '../../data/models/add_prescription_confirm_model.dart';

class AddPrescriptionsConfirm extends StatefulWidget {
  const AddPrescriptionsConfirm({
    super.key,
    required this.addPrescriptionConfirmModel,
  });

  final AddPrescriptionConfirmModel addPrescriptionConfirmModel;

  @override
  State<AddPrescriptionsConfirm> createState() =>
      _AddPrescriptionsConfirmState();
}

class _AddPrescriptionsConfirmState extends State<AddPrescriptionsConfirm> {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: SingleChildScrollView(
        child: Padding(
          padding: const EdgeInsets.only(
            top: 4.0,
            bottom: 16,
            left: 16,
            right: 16,
          ),
          child: Column(
            children: [
              CustomAppBarPopWidget(text: S.of(context).addPrescription),
              SizedBox(height: 32),
              AddPrescriptionCard(
                isConfirm: true,
                patientNameController:
                    widget.addPrescriptionConfirmModel.patientName,
                dateController:
                    widget.addPrescriptionConfirmModel.dateController,
                ageController: widget.addPrescriptionConfirmModel.ageController,
                prescriptionList: widget.addPrescriptionConfirmModel.medicineList,
              ),
              SizedBox(height: 16),
              CustomFullButton(
                text: S.of(context).send,
                onPressed: () {},
                radios: 8,
              ),
            ],
          ),
        ),
      ),
    );
  }
}
