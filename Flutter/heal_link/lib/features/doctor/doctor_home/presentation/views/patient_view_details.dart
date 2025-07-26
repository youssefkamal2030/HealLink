import 'package:flutter/material.dart';
import 'package:go_router/go_router.dart';
import 'package:heal_link/core/utils/app_router.dart';
import 'package:heal_link/features/doctor/doctor_home/presentation/views/widgets/patient_action_widget.dart';
import 'package:heal_link/features/doctor/doctor_home/presentation/views/widgets/patient_details_card.dart';
import 'package:heal_link/features/doctor/doctor_home/presentation/views/widgets/patient_history_uploaded_report.dart';
import '../../../../../core/utils/app_images.dart';
import '../../../../../core/widgets/custom_app_bar_pop_widget.dart';
import '../../../../../generated/l10n.dart';

class PatientViewDetails extends StatelessWidget {
  const PatientViewDetails({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: SingleChildScrollView(
        child: Padding(
          padding: EdgeInsets.symmetric(horizontal: 16.0),
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            spacing: 24,
            children: [
              CustomAppBarPopWidget(text: S.of(context).viewDetails),
              PatientDetailsCard(),
              Row(
                spacing: 16,
                children: [
                  PatientActionWidget(
                    image: AppImages.message,
                    text: S.of(context).message,
                  ),
                  InkWell(
                    onTap: () => context.push(AppRouter.addPrescriptionView),
                    child: PatientActionWidget(
                      image: AppImages.prescriptions,
                      text: S.of(context).addPrescription,
                    ),
                  ),
                ],
              ),
              PatientHistoryUploadedReport(),
            ],
          ),
        ),
      ),
    );
  }
}
