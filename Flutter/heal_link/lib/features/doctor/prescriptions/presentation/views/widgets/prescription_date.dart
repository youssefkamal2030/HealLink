import 'package:flutter/cupertino.dart';
import 'package:heal_link/features/doctor/prescriptions/presentation/views/widgets/prescription_list_tile.dart';
import 'package:heal_link/generated/l10n.dart';
import 'package:heal_link/core/utils/app_styles.dart';

class PrescriptionDate extends StatelessWidget {
  const PrescriptionDate({super.key});

  @override
  Widget build(BuildContext context) {
    return Column(
      spacing: 8,
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        Text(
          S.of(context).thisMonth,
          style: AppTextStyles.popins500style16PrimaryColor,
        ),
        PrescriptionListTile(),
        PrescriptionListTile(),
        Text('Jun', style: AppTextStyles.popins500style16PrimaryColor),
        PrescriptionListTile(),
        PrescriptionListTile(),
      ],
    );
  }
}
