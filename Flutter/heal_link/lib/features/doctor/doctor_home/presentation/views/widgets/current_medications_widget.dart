import 'package:flutter/material.dart';
import 'package:flutter_svg/svg.dart';

import '../../../../../../core/utils/app_images.dart';
import '../../../../../../core/utils/app_styles.dart';
import '../../../../../../core/utils/function/app_colors.dart';

class CurrentMedicationsWidget extends StatelessWidget {
  const CurrentMedicationsWidget({
    super.key,
  });

  @override
  Widget build(BuildContext context) {
    return Stack(
      children: [
        Container(
          height: 210,
          decoration: BoxDecoration(
            color: Colors.white,
            borderRadius: BorderRadius.circular(16),
          ),
        ),
        Container(
          padding: EdgeInsets.all(8),
          height: 65,
          decoration: BoxDecoration(
            borderRadius: BorderRadius.only(
              topRight: Radius.circular(16),
              topLeft: Radius.circular(16),
            ),
            color: Colors.white,
            gradient: LinearGradient(
              colors: [Color(0xFFDBEBFF), Color(0xFFFFFFFF)],
              begin: AlignmentDirectional.centerEnd,
              end: AlignmentDirectional.centerStart,
              stops: [-0.7126, 0.7791],
            ),
            boxShadow: [
              BoxShadow(
                color: Color(0x0D000000),
                blurRadius: 5,
                offset: Offset(1, 1),
              ),
            ],
          ),
          child: Row(
            children: [
              SizedBox(width: 8),
              SvgPicture.asset(AppImages.prescriptions),
              SizedBox(width: 14),
              Column(
                crossAxisAlignment: CrossAxisAlignment.start,
                children: [
                  Text(
                    'Glucophage',
                    style: AppTextStyles.popins400style16LightDarkGrey
                        .copyWith(color: AppColors.kLightBlackColor),
                  ),
                  Text(
                    '02/05/2025',
                    style: AppTextStyles.popins400style14LightBlackColor
                        .copyWith(color: AppColors.kDarkGreyColor),
                  ),
                ],
              ),
            ],
          ),
        ),
        Positioned(
          top: 76,
          child: Padding(
            padding: const EdgeInsets.only(left: 16.0),
            child: Column(
              mainAxisSize: MainAxisSize.min,
              mainAxisAlignment: MainAxisAlignment.start,
              crossAxisAlignment: CrossAxisAlignment.start,
              spacing: 8,
              children: [
                Row(
                  spacing: 4,
                  children: [
                    Text(
                      'Type :',
                      style: AppTextStyles.popins500style12BlackColor,
                    ),
                    Text(
                      'Antidiabetic ',
                      style: AppTextStyles
                          .popins400style12LightBlackColor
                          .copyWith(color: AppColors.kBlackColor),
                    ),
                  ],
                ),
                Row(
                  spacing: 4,
                  children: [
                    Text(
                      'Active Ingredient : ',
                      style: AppTextStyles.popins500style12BlackColor,
                    ),
                    Text(
                      'Metformin Hydrochloride ',
                      style: AppTextStyles
                          .popins400style12LightBlackColor
                          .copyWith(color: AppColors.kBlackColor),
                    ),
                  ],
                ),
                Row(
                  spacing: 4,
                  children: [
                    Text(
                      'Dosage :',
                      style: AppTextStyles.popins500style12BlackColor,
                    ),
                    Text(
                      'Once or Twice Daily ',
                      style: AppTextStyles
                          .popins400style12LightBlackColor
                          .copyWith(color: AppColors.kBlackColor),
                    ),
                  ],
                ),
                Row(
                  spacing: 4,
                  children: [
                    Text(
                      'Duration of Therapy  : ',
                      style: AppTextStyles.popins500style12BlackColor,
                    ),
                    Text(
                      'long- time',
                      style: AppTextStyles
                          .popins400style12LightBlackColor
                          .copyWith(color: AppColors.kBlackColor),
                    ),
                  ],
                ),
              ],
            ),
          ),
        ),
      ],
    );
  }
}
