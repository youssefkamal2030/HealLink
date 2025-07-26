import 'package:flutter/material.dart';
import 'package:heal_link/core/utils/function/app_colors.dart';
import '../../../../../../core/utils/app_styles.dart';
import '../../../../../../generated/l10n.dart';
import 'custom_circle_image.dart';

class PatientDetails extends StatelessWidget {
  const PatientDetails({
    super.key,
    required this.image,
    required this.name,
  });

  final String image;
  final String name;

  @override
  Widget build(BuildContext context) {
    return Row(
      children: [
        CustomCircleImage(image: image, isVerified: false, radius: 44),
        SizedBox(width: 8),
        Column(
          mainAxisSize: MainAxisSize.min,
          crossAxisAlignment: CrossAxisAlignment.start,
          spacing: 4,
          children: [
            Text(name, style: AppTextStyles.popins500style18LightBlackColor),
            Text.rich(
              TextSpan(
                style: AppTextStyles.popins500style18LightBlackColor.copyWith(
                  fontSize: 16,
                  color: AppColors.kPrimaryColor,
                ),
                children: [
                  TextSpan(text: '${S.of(context).gender} : '),
                  TextSpan(
                    text: 'Male',
                    style: AppTextStyles.popins400style14LightBlackColor,
                  ),
                  TextSpan(text: '      ${S.of(context).age} : '),
                  TextSpan(
                    text: '25',
                    style: AppTextStyles.popins400style14LightBlackColor,
                  ),
                ],
              ),
            ),
          ],
        ),
      ],
    );
  }
}
