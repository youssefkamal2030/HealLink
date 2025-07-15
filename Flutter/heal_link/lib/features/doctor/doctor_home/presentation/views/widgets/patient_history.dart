import 'package:flutter/material.dart';

import '../../../../../../core/utils/app_styles.dart';
import '../../../../../../generated/l10n.dart';
import 'custom_patient_history_row.dart';

class PatientHistory extends StatelessWidget {
  const PatientHistory({
    super.key,
  });

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
        CustomPatientHistoryRow(text: S.of(context).chronic_diseases),
        CustomPatientHistoryRow(text: S.of(context).allergies),
        CustomPatientHistoryRow(text: S.of(context).past_surgeries),
        CustomPatientHistoryRow(
          text: S.of(context).current_medications,
        ),
        Divider(height: 0.5,thickness: 0.5,),
        SizedBox(height: 16),
        Text(
          S.of(context).uploaded_reports_files,
          style: AppTextStyles.popins500style16PrimaryColor,
        ),
        SizedBox(height: 8),
        CustomPatientHistoryRow(text: S.of(context).lab_tests,isLabTest: true,),
        CustomPatientHistoryRow(text: S.of(context).x_rays),
        Divider(height: 0.5,thickness: 0.5,),
      ],
    );
  }
}
