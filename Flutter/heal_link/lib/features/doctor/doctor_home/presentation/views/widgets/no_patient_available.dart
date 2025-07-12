import 'package:flutter/material.dart';
import 'package:flutter_svg/svg.dart';

import '../../../../../../core/utils/app_images.dart';
import '../../../../../../core/utils/app_styles.dart';
import '../../../../../../core/utils/function/app_colors.dart';
import '../../../../../../generated/l10n.dart';

class NoPatientAvailable extends StatelessWidget {
  const NoPatientAvailable({
    super.key,
  });

  @override
  Widget build(BuildContext context) {
    return Column(
      crossAxisAlignment: CrossAxisAlignment.center,
      children: [
        SizedBox(height: 4),
        SvgPicture.asset(AppImages.noPatient),
        Text(
          S.of(context).no_patients_at_moment,
          style: AppTextStyles.popins400style12kPrimaryColor
              .copyWith(fontSize: 16),
          textAlign: TextAlign.center,
        ),
        SizedBox(height: 8),
        Text(
          S.of(context).once_patients_subscribing,
          style: AppTextStyles.popins400style12kPrimaryColor
              .copyWith(color: AppColors.kBlackColor),
          textAlign: TextAlign.center,
        ),
        SizedBox(height: 30)
      ],
    );
  }
}
