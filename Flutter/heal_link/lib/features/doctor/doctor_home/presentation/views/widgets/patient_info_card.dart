import 'package:flutter/material.dart';

import '../../../../../../core/utils/app_images.dart';
import '../../../../../../core/utils/app_styles.dart';
import '../../../../../../core/utils/function/app_colors.dart';
import '../../../../../../core/widgets/custom_empty_button.dart';
import 'custom_circle_image.dart';

class PatientInfoCard extends StatelessWidget {
  const PatientInfoCard({super.key, required this.response});

  final void Function() response;

  @override
  Widget build(BuildContext context) {
    return Container(
      padding: EdgeInsets.all(8),
      decoration: BoxDecoration(
        color: AppColors.kWhiteColor,
        borderRadius: BorderRadius.circular(8),
        boxShadow: [
          BoxShadow(
            offset: Offset(1, 1),
            blurRadius: 4,
            color: Color(0x14000000),
          ),
        ],
      ),
      height: 81,
      child: Row(
        spacing: 8,
        children: [
          CustomCircleImage(
            image: AppImages.person,
            isVerified: false,
            radius: 25,
          ),
          Column(
            spacing: 4,
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              Text(
                'Ahmed Omar',
                style: AppTextStyles.popins500style20LightBlackColor.copyWith(
                  fontSize: 14,
                ),
              ),
              Text.rich(
                TextSpan(
                  children: [
                    TextSpan(
                      text: 'Last interaction: ',
                      style: AppTextStyles.popins400style12kPrimaryColor,
                    ),
                    TextSpan(
                      text: 'Jun 29',
                      style: AppTextStyles.popins400style12kPrimaryColor
                          .copyWith(color: AppColors.kDarkGreyColor),
                    ),
                  ],
                ),
              ),
              Text.rich(
                TextSpan(
                  children: [
                    TextSpan(
                      text: 'Type: ',
                      style: AppTextStyles.popins400style12kPrimaryColor,
                    ),
                    TextSpan(
                      text: 'Consultation',
                      style: AppTextStyles.popins400style12kPrimaryColor
                          .copyWith(color: AppColors.kDarkGreyColor),
                    ),
                  ],
                ),
              ),
            ],
          ),
          Flexible(
            child: Align(
              alignment: AlignmentDirectional.centerEnd,
              child: ConstrainedBox(
                constraints: BoxConstraints(maxWidth: 140),
                child: CustomEmptyButton(
                  borderRadius: 8,
                  height: 34,
                  borderSide: 1,
                  text: 'View Details',
                  response: response,
                  textStyle: AppTextStyles.popins400style14LightBlackColor
                      .copyWith(color: AppColors.kPrimaryColor),
                  iconSize: 15,
                ),
              ),
            ),
          ),
        ],
      ),
    );
  }
}
