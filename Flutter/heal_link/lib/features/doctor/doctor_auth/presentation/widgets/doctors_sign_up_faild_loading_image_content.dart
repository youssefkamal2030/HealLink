
import 'package:flutter/material.dart';
import 'package:flutter_svg/svg.dart';
import 'package:heal_link/core/utils/app_images.dart';
import 'package:heal_link/core/utils/app_styles.dart';
import 'package:heal_link/core/utils/function/app_colors.dart';

class DoctorSignUpFaildUploadingImageContent extends StatelessWidget {
  const DoctorSignUpFaildUploadingImageContent({super.key});

  @override
  Widget build(BuildContext context) {
    return ListTile(
      contentPadding: EdgeInsets.all(0),

      leading: SvgPicture.asset(
        AppImages.documentTextIcon,

        colorFilter: ColorFilter.mode(
          AppColors.kDarkErrorColor,
          BlendMode.srcIn,
        ),
      ),
      trailing: SvgPicture.asset(
        AppImages.trashIcon,

        colorFilter: ColorFilter.mode(
          AppColors.kDarkErrorColor,
          BlendMode.srcIn,
        ),
      ),
      title: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          Text(
            "Download failed, please try again.",
            style: AppTextStyles.popins400style14LightBlackColor.copyWith(
              color: AppColors.kDarkErrorColor,
            ),
          ),
          Text(
            "Moatasm_Resume.Jpg",
            style: AppTextStyles.popins400style12LightBlackColor.copyWith(
              color: AppColors.kDarkErrorColor,
            ),
          ),
        ],
      ),
      subtitle: Text(
        "try again ",
        style: AppTextStyles.popins400style14LightBlackColor.copyWith(
          color: AppColors.kDarkErrorColor,
          fontWeight: FontWeight.w500,
        ),
      ),
    );
  }
}


