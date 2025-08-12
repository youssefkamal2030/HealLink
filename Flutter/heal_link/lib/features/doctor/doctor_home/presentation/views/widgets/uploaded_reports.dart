import 'package:flutter/material.dart';
import 'package:go_router/go_router.dart';

import '../../../../../../core/utils/app_router.dart';
import '../../../../../../core/utils/app_styles.dart';
import '../../../../../../generated/l10n.dart';
import 'custom_patient_history_row.dart';

class UploadedReports extends StatelessWidget {
  const UploadedReports({super.key});

  @override
  Widget build(BuildContext context) {
    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      mainAxisAlignment: MainAxisAlignment.start,
      children: [
        Text(
          S.of(context).uploaded_reports_files,
          style: AppTextStyles.popins500style16PrimaryColor,
        ),
        SizedBox(height: 8),
        CustomPatientHistoryRow(
          text: S.of(context).prescriptions,
          onTap: () => context.push(AppRouter.prescriptions),
        ),
        InkWell(
          onTap: () {
            context.push(AppRouter.labTestView);
          },
          child: CustomPatientHistoryRow(
            text: S.of(context).lab_tests,
            isLabTest: true,
          ),
        ),
        InkWell(
          onTap: () {
            context.push(AppRouter.radiologyView);
          },
          child: CustomPatientHistoryRow(text: S.of(context).x_rays),
        ),
        Divider(height: 0.5, thickness: 0.5),
      ],
    );
  }
}
