
import 'package:flutter/material.dart';
import 'package:flutter_svg/svg.dart';
import 'package:heal_link/core/utils/app_images.dart';
import 'package:heal_link/core/utils/app_styles.dart';
import 'package:heal_link/core/utils/function/app_colors.dart';
import 'package:heal_link/core/widgets/custom_progress_indicator.dart';

class DoctorSignUpUpLoadingImageContent extends StatelessWidget {
  const DoctorSignUpUpLoadingImageContent({
    super.key,
    required this.uploaded,
    required this.progress ,
  });
  final bool uploaded;
  final double progress;
  @override
  Widget build(BuildContext context) {
    return Column(
      mainAxisAlignment: MainAxisAlignment.start,
      children: [
        ListTile(
          contentPadding: EdgeInsets.all(0),

          leading: SvgPicture.asset(
            AppImages.documentTextIcon,

            colorFilter: ColorFilter.mode(
              AppColors.kBlackColor,
              BlendMode.srcIn,
            ),
          ),
          trailing:
              uploaded
                  ? SvgPicture.asset(AppImages.tickGreenIcon)
                  : SvgPicture.asset(
                    AppImages.trashIcon,

                    colorFilter: ColorFilter.mode(
                      AppColors.kBlackColor,
                      BlendMode.srcIn,
                    ),
                  ),
          title: Text(
            "Moatasm_Resume.pdf",
            style: AppTextStyles.popins400style14LightBlackColor,
          ),
          subtitle: Text(
            "200 KB",
            style: AppTextStyles.popins400style12LightBlackColor.copyWith(
              color: AppColors.kDarkGreyColor,
            ),
          ),
        ),
        CustomProgressBar(
          progress: progress,
          indicatorColor:
              uploaded ? AppColors.kGreenColor : AppColors.kPrimaryColor,
        ),
      ],
    );
  }
}
