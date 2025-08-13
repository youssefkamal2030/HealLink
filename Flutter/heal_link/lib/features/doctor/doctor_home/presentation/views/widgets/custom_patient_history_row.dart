import 'package:flutter/material.dart';
import 'package:heal_link/core/utils/app_styles.dart';
import 'package:heal_link/core/utils/function/app_colors.dart';

class CustomPatientHistoryRow extends StatelessWidget {
  const CustomPatientHistoryRow(
      {super.key, required this.text, this.isLabTest = false, this.onTap});

  final String text;
  final bool ?isLabTest;
  final Function()? onTap;

  @override
  Widget build(BuildContext context) {
    return InkWell(
      onTap: onTap,
      child: SizedBox(
        height: 32,
        child: Row(
          children: [
            Text(text, style: AppTextStyles.popins400style14LightBlackColor),
            Spacer(),
            if(isLabTest!)
              CircleAvatar(
                radius: 10,
                backgroundColor: AppColors.kErrorColor,
                child: Text(
                  '2',
                  style: AppTextStyles.popins400style12kPrimaryColor.copyWith(
                    color: AppColors.kWhiteColor,
                  ),
                ),
              ),
            Icon(
              Icons.arrow_forward_ios,
              size: 20,
              color: AppColors.kLightBlackColor,
            ),
          ],
        ),
      ),
    );
  }
}
