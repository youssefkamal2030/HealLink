import 'package:flutter/material.dart';
import 'package:heal_link/core/utils/app_images.dart';
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
            image: AppImages.onBoardingImage1,
            title: S.of(context).invite_family,
            subTitle: S.of(context).choose_role,
          ),
          Positioned(
            top: AppConstant.height * 0.8,
            right: 0,
            left: 0,
            child: Padding(
              padding: const EdgeInsets.symmetric(horizontal: 16.0),
              child: Column(
                children: [
                  CustomEmptyButton(
                    text: S.of(context).i_am_patient,
                    response: () {},
                    height: 39,
                  ),
                  SizedBox(height: 10,),
                  CustomEmptyButton(
                    text: S.of(context).i_am_doctor,
                    height: 39,
                    response: () {},
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
