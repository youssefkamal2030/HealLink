import 'package:flutter/cupertino.dart';
import 'package:heal_link/core/utils/app_images.dart';
import 'package:heal_link/features/doctor/prescriptions/presentation/views/widgets/custom_list_tile.dart';
import 'package:heal_link/generated/l10n.dart';
import 'package:heal_link/core/utils/app_styles.dart';

class RadiologyWidget extends StatelessWidget {
  const RadiologyWidget({super.key});

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
          img: AppImages.radiology,
          title: S.of(context).echocardiogram,
          date: '02/05/2025',
        ),
        CustomListTile(
          img: AppImages.radiology,
          title: S.of(context).chest_xray,
          date: '02/05/2025',
        ),

        Text('Jun', style: AppTextStyles.popins500style16PrimaryColor),
        CustomListTile(
          img: AppImages.radiology,
          title: S.of(context).ct_coronary_angiography,
          date: '02/05/2025',
        ),
        CustomListTile(
          img: AppImages.radiology,
          title: S.of(context).cardiac_mri,
          date: '02/05/2025',
        ),
      ],
    );
  }
}
