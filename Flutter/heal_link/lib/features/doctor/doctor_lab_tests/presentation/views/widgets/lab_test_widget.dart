import 'package:flutter/cupertino.dart';
import 'package:heal_link/core/utils/app_images.dart';
import 'package:heal_link/features/doctor/prescriptions/presentation/views/widgets/custom_list_tile.dart';
import 'package:heal_link/generated/l10n.dart';
import 'package:heal_link/core/utils/app_styles.dart';

class LabTestWidget extends StatelessWidget {
  const LabTestWidget({super.key});

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
        CustomListTile(
          img: AppImages.labTest,
          title: S.of(context).lipid_profile,
          date: '02/05/2025',
        ),
        CustomListTile(
          img: AppImages.labTest,
          title: S.of(context).cardiac_enzymes,
          date: '02/05/2025',
        ),

        Text('Jun', style: AppTextStyles.popins500style16PrimaryColor),
        CustomListTile(
          img: AppImages.labTest,
          title: S.of(context).kidney_function,
          date: '02/05/2025',
        ),
        CustomListTile(
          img: AppImages.labTest,
          title: S.of(context).blood_sugar,
          date: '02/05/2025',
        ),
      ],
    );
  }
}
