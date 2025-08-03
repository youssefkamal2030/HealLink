import 'package:flutter/material.dart';
import 'package:heal_link/core/utils/function/custom_awesome_dialog.dart';
import 'package:heal_link/features/doctor/doctor_home/data/models/add_prescription_model.dart';
import 'package:heal_link/features/doctor/doctor_home/presentation/views/widgets/added_prescription_item.dart';
import 'add_prescription_body_bottom.dart';
import 'add_prescription_body_top.dart';

class AddPrescriptionBody extends StatefulWidget {
  const AddPrescriptionBody({
    super.key,
    required this.isConfirm,
    required this.patientNameController,
    required this.ageController,
    required this.dateController,
    required this.prescriptionList,
  });

  final List<AddPrescriptionModel> prescriptionList;
  final TextEditingController patientNameController;
  final TextEditingController ageController;
  final TextEditingController dateController;
  final bool isConfirm;

  @override
  State<AddPrescriptionBody> createState() => _AddPrescriptionBodyState();
}

class _AddPrescriptionBodyState extends State<AddPrescriptionBody> {
  TextEditingController frequencyController = TextEditingController(text: '1');
  TextEditingController medicineNameController = TextEditingController();
  TextEditingController instructionsController = TextEditingController();

  var formKey = GlobalKey<FormState>();

  @override
  Widget build(BuildContext context) {
    return Positioned(
      top: 46,
      left: 24,
      right: 24,
      child: Form(
        key: formKey,
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          spacing: 8,
          children: [
            AddPrescriptionBodyTop(
              ageController: widget.ageController,
              dateController: widget.dateController,
              patientNameController: widget.patientNameController,
            ),
            SizedBox(height: 90 - 16 - 8),
            if (!widget.isConfirm && widget.prescriptionList.isNotEmpty)
              AddedPrescriptionItem(
                addPrescriptionModel:
                widget.prescriptionList[widget.prescriptionList.length - 1],
                index: widget.prescriptionList.length,
              ),
            if (widget.isConfirm)
              ListView.builder(
                physics: NeverScrollableScrollPhysics(),
                shrinkWrap: true,
                padding: EdgeInsets.zero,
                itemBuilder: (context, index) {
                  return Column(
                    children: [
                      AddedPrescriptionItem(
                        addPrescriptionModel: widget.prescriptionList[index],
                        index: index + 1,
                      ),
                      SizedBox(height: 16),
                    ],
                  );
                },
                itemCount: widget.prescriptionList.length,
              ),

            SizedBox(height: 19 - 8),
            if (!widget.isConfirm)
              AddPrescriptionBodyBottom(
                frequencyController: frequencyController,
                instructionsController: instructionsController,
                medicineNameController: medicineNameController,
                formKey: formKey,
                onAddPressed: () {
                  if (medicineNameController.text.isNotEmpty &&
                      frequencyController.text.isNotEmpty &&
                      instructionsController.text.isNotEmpty &&
                      !isNameDuplicated(
                        widget.prescriptionList,
                        medicineNameController.text,
                      )) {
                    AddPrescriptionModel addPrescriptionModel =
                    AddPrescriptionModel(
                      medicineName: medicineNameController.text,
                      frequencyofUse: frequencyController.text,
                      usageInstructions: instructionsController.text,
                    );
                    widget.prescriptionList.add(addPrescriptionModel);
                  } else {
                    isNameDuplicated(
                      widget.prescriptionList,
                      medicineNameController.text,
                    )
                        ? customAwesomeDialog(
                        context, message: 'Medicine name is already found')
                        : customAwesomeDialog(context,
                        message: 'Please Enter medicine information to proceed');
                  }
                  setState(() {});
                },
              ),
          ],
        ),
      ),
    );
  }
}

bool isNameDuplicated(List<AddPrescriptionModel> prescriptionModel,
    String medicineName,) {
  for (var element in prescriptionModel) {
    if (element.medicineName == medicineName) {
      return true;
    }
  }
  return false;
}
