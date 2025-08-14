import 'package:flutter/material.dart';
import 'package:go_router/go_router.dart';
import 'package:heal_link/features/doctor/doctor_home/presentation/views/widgets/uploaded_reports.dart';

import '../../../../../../core/utils/app_router.dart';
import '../../../../../../core/utils/app_styles.dart';
import '../../../../../../generated/l10n.dart';
import 'custom_patient_history_row.dart';

class PatientHistoryUploadedReport extends StatelessWidget {
  const PatientHistoryUploadedReport({super.key});

  @override
  Widget build(BuildContext context) {
    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        Text(
          S.of(context).patient_history,
          style: AppTextStyles.popins500style16PrimaryColor,
        ),
        SizedBox(height: 8),
        CustomPatientHistoryRow(
          text: S.of(context).chronic_diseases,
          onTap: () => context.push(AppRouter.chronicDiseasesView),
        ),
        CustomPatientHistoryRow(
          text: S.of(context).allergies,
          onTap: () => context.push(AppRouter.allergiesView),
        ),
        CustomPatientHistoryRow(
          text: S.of(context).past_surgeries,
          onTap: () => context.push(AppRouter.pastSurgeriesView),
        ),
        CustomPatientHistoryRow(
          text: S.of(context).current_medications,
          onTap: () => context.push(AppRouter.currentMedicationsView),
        ),
        Divider(height: 0.5, thickness: 0.5),
        SizedBox(height: 16),
        UploadedReports(),
      ],
    );
  }
}
