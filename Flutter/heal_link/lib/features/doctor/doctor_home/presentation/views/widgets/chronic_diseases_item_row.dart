import 'package:flutter/material.dart';

import '../../../../../../core/utils/app_styles.dart';
import '../../../../../../core/utils/function/app_colors.dart';

class ChronicDiseasesItemRow extends StatelessWidget {
  const ChronicDiseasesItemRow({
    super.key,
    required this.title,
    required this.subTitle,
    this.titleColor,
    this.subTitleColor,
  });

  final String title;
  final String subTitle;
  final Color? titleColor;
  final Color? subTitleColor;

  @override
  Widget build(BuildContext context) {
    return Row(
      spacing: 8,
      children: [
        Text(
          title,
          textAlign: TextAlign.center,
          style: AppTextStyles.popins500style14LightBlackColor.copyWith(
            color: titleColor ?? AppColors.kPrimaryColor,
          ),
        ),
        Expanded(
          child: Text(
            subTitle,
            textAlign: TextAlign.start,
            style: AppTextStyles.popins400style12LightBlackColor.copyWith(
              color: subTitleColor ?? AppColors.kDarkGreyColor,
            ),
            maxLines: 1,
            overflow: TextOverflow.ellipsis,
          ),
        ),
      ],
    );
  }
}