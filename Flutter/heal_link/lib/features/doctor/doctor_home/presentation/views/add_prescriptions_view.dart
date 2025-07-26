import 'package:flutter/material.dart';
import 'package:go_router/go_router.dart';
import 'package:heal_link/core/utils/app_router.dart';
import 'package:heal_link/core/utils/function/custom_awesome_dialog.dart';
import 'package:heal_link/core/widgets/custom_full_button.dart';
import 'package:heal_link/features/doctor/doctor_home/data/models/add_prescription_confirm_model.dart';
import 'package:heal_link/features/doctor/doctor_home/presentation/views/widgets/add_prescription_card.dart';
import '../../../../../core/widgets/custom_app_bar_pop_widget.dart';
import '../../../../../generated/l10n.dart';
import '../../data/models/add_prescription_model.dart';

class AddPrescriptionsView extends StatefulWidget {
  const AddPrescriptionsView({super.key});

  @override
  State<AddPrescriptionsView> createState() => _AddPrescriptionsViewState();
}

class _AddPrescriptionsViewState extends State<AddPrescriptionsView> {
  final List<AddPrescriptionModel> prescriptionList = [];
  TextEditingController patientNameController = TextEditingController();
  TextEditingController ageController = TextEditingController();
  TextEditingController dateController = TextEditingController(
    text:
        '${DateTime.now().day} / ${DateTime.now().month} / ${DateTime.now().year}',
  );

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
                isConfirm: false,
                patientNameController: patientNameController,
                dateController: dateController,
                ageController: ageController,
                prescriptionList: prescriptionList,
              ),
              SizedBox(height: 16),
              CustomFullButton(
                text: S.of(context).next,
                onPressed: () {
                  if (prescriptionList.isNotEmpty &&
                      patientNameController.text.isNotEmpty &&
                      dateController.text.isNotEmpty &&
                      ageController.text.isNotEmpty) {
                    AddPrescriptionConfirmModel model =
                        AddPrescriptionConfirmModel(
                          ageController,
                          patientNameController,
                          prescriptionList,
                          dateController: dateController,
                        );
                    context.push(AppRouter.addPrescriptionConfirm, extra: model);
                  }
                  else if(patientNameController.text.isEmpty ||
                      dateController.text.isEmpty ||
                      ageController.text.isEmpty){
                    customAwesomeDialog(context, message: 'Please fill patient Data');
                  }
                  else{
                    customAwesomeDialog(context, message: 'Please add atLeast one medicine');
                  }
                },
                radios: 8,
              ),
            ],
          ),
        ),
      ),
    );
  }
}
