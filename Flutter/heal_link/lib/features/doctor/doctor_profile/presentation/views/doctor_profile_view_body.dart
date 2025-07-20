import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:heal_link/core/utils/app_images.dart';
import 'package:heal_link/core/utils/app_styles.dart';
import 'package:heal_link/core/utils/function/app_colors.dart';
import 'package:heal_link/features/doctor/doctor_profile/presentation/views/widgets/profile_list_tile.dart';
import 'package:heal_link/generated/l10n.dart';

import '../../../doctor_home/presentation/views/widgets/custom_circle_image.dart';

class DoctorProfileViewBody extends StatelessWidget {
  const DoctorProfileViewBody({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Padding(
        padding: const EdgeInsets.symmetric(horizontal: 19.0),
        child: Column(
          children: [
            SizedBox(height: 68),
            Text(
              S.of(context).my_profile,
              style: AppTextStyles.popins500style20LightBlackColor,
            ),
            SizedBox(height: 16),
            Container(
              height: 93,
              padding: EdgeInsets.symmetric(vertical: 16, horizontal: 8),
              decoration: BoxDecoration(
                color: Colors.white,
                borderRadius: BorderRadius.circular(8),
              ),
              child: Row(
                spacing: 12,
                children: [
                  CustomCircleImage(
                    image: AppImages.person,
                    isVerified: true,
                    radius: 31,
                  ),
                  Column(
                    crossAxisAlignment: CrossAxisAlignment.start,
                    mainAxisAlignment: MainAxisAlignment.center,
                    children: [
                      Text(
                        'Dr. Yousry Ahmed',
                        style: AppTextStyles.popins500style18LightBlackColor,
                      ),
                      Text(
                        S.of(context).change_profile,
                        style: AppTextStyles.popins400style12LightBlackColor
                            .copyWith(
                              decoration: TextDecoration.underline,
                              color: AppColors.kDarkGreyColor,
                            ),
                      ),
                    ],
                  ),
                  Spacer(),
                  SvgPicture.asset(
                    AppImages.edit,
                    colorFilter: ColorFilter.mode(
                      AppColors.kPrimaryColor,
                      BlendMode.srcIn,
                    ),
                  ),
                ],
              ),
            ),
            SizedBox(height: 16),
            ProfileListTile(
              img: AppImages.personalInfo,
              title: S.of(context).personal_information,
            ),
            SizedBox(height: 8),
            ProfileListTile(
              img: AppImages.notify,
              title: S.of(context).notification,
            ),
            SizedBox(height: 8),
            ProfileListTile(
              img: AppImages.payment,
              title: S.of(context).payment,
            ),
            SizedBox(height: 8),
            ProfileListTile(
              img: AppImages.terms,
              title: S.of(context).terms_and_privacy_policy,
            ),
            SizedBox(height: 8),
            ProfileListTile(
              img: AppImages.support,
              title: S.of(context).support_help,
            ),
            SizedBox(height: 8),
            ProfileListTile(img: AppImages.about, title: S.of(context).about),
          ],
        ),
      ),
    );
  }
}
