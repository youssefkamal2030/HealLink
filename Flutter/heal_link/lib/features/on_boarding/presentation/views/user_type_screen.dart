import 'package:flutter/material.dart';
import 'package:go_router/go_router.dart';
import 'package:heal_link/core/utils/app_images.dart';
import 'package:heal_link/core/utils/app_router.dart';
import 'package:heal_link/core/utils/app_styles.dart';
import 'package:heal_link/core/utils/function/app_colors.dart';
import 'package:heal_link/core/widgets/custom_empty_button.dart';
import 'package:heal_link/features/on_boarding/presentation/views/widgets/on_boarding_bage_view.dart';
import '../../../../core/utils/constant.dart';
import '../../../../generated/l10n.dart';

class UserTypeScreen extends StatelessWidget {
  const UserTypeScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Stack(
        children: [
          OnBoardingPageView(
            image: AppImages.onBoardingImage3,
            title: S.of(context).invite_family,
            subTitle: S.of(context).choose_role,
            topPadding: AppConstant.height * .22,
            rightPadding: 10,
            subTitleTextStyle: AppTextStyles.popins400style14LightBlackColor
                .copyWith(color: AppColors.kPrimaryColor),
          ),
          Positioned(
            top: AppConstant.height * 0.81,
            right: 0,
            left: 0,
            child: Padding(
              padding: const EdgeInsets.symmetric(horizontal: 16.0),
              child: Column(
                children: [
                  CustomEmptyButton(
                    text: S.of(context).i_am_patient,
                    response: () {
                      context.push(AppRouter.patientSignUpView);
                    },
                    height: 44,
                    borderRadius: 16,
                  ),
                  SizedBox(height: 16),
                  CustomEmptyButton(
                    text: S.of(context).i_am_doctor,
                    response: () {
                      context.push(AppRouter.doctorSignUpFirstView);
                    },
                    height: 44,
                    borderRadius: 16,
                  ),
                ],
              ),
            ),
          ),
        ],
      ),
    );
  }
}
