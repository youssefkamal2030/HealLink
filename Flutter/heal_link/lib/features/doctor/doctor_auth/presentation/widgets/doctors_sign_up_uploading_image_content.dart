
import 'package:flutter/material.dart';
import 'package:flutter_svg/svg.dart';
import 'package:heal_link/core/utils/app_images.dart';
import 'package:heal_link/core/utils/app_styles.dart';
import 'package:heal_link/core/utils/function/app_colors.dart';
import 'package:heal_link/generated/l10n.dart';

class DoctorSignUpUploadImageContent extends StatelessWidget {
  const DoctorSignUpUploadImageContent({super.key});

  @override
  Widget build(BuildContext context) {
    return GestureDetector(
      onTap: () {},
      child: Column(
        mainAxisAlignment: MainAxisAlignment.center,
        crossAxisAlignment: CrossAxisAlignment.center,
        children: [
          SvgPicture.asset(AppImages.uploadImageIcon),
          SizedBox(height: 8),
          Text(
            S.of(context).click_to_upload,
            style: AppTextStyles.popins400style14LightBlackColor.copyWith(
              color: AppColors.kPrimaryColor,
            ),
          ),
        ],
      ),
    );
  }
}

