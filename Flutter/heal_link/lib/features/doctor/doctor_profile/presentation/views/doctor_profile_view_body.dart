import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:heal_link/core/utils/app_images.dart';
import 'package:heal_link/core/utils/app_styles.dart';
import 'package:heal_link/core/utils/function/app_colors.dart';
import 'package:heal_link/core/widgets/custom_app_bar.dart';
import 'package:heal_link/features/doctor/doctor_profile/presentation/views/widgets/profile_list_tile.dart';
import 'package:heal_link/generated/l10n.dart';

class DoctorProfileViewBody extends StatelessWidget {
  const DoctorProfileViewBody({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: CustomAppBar(
        title: S.of(context).my_profile,
        showBackButton: false,
      ),

      body: Padding(
        padding: const EdgeInsets.all(8.0),
        child: Column(
          children: [
            Container(
              decoration: BoxDecoration(
                color: Colors.white,
                borderRadius: BorderRadius.circular(8),
              ),
              child: ListTile(
                leading: Stack(
                  clipBehavior: Clip.none,
                  children: [
                    CircleAvatar(
                      radius: 40,
                      child: Image.asset(AppImages.person),
                    ),
                    Positioned(
                      bottom: -2,
                      right: -2,
                      child: Container(
                        decoration: BoxDecoration(
                          shape: BoxShape.circle,
                          color: Colors.white,
                        ),
                        padding: const EdgeInsets.all(2),
                        child: SvgPicture.asset(AppImages.verify),
                      ),
                    ),
                  ],
                ),
                title: Text(
                  S.of(context).personal_information,
                  style: AppTextStyles.popins500style18LightBlackColor,
                ),
                subtitle: Text(
                  'Change Profile',
                  style: AppTextStyles.popins400style12LightBlackColor.copyWith(
                    decoration: TextDecoration.underline,
                    color: AppColors.kDarkGreyColor,
                  ),
                ),
                trailing: SvgPicture.asset(AppImages.edit),
              ),
            ),
            SizedBox(height: 10),
            ProfileListTile(
              img: AppImages.personalInfo,
              title: S.of(context).personal_information,
            ),
            SizedBox(height: 10),
            ProfileListTile(
              img: AppImages.notify,
              title: S.of(context).notification,
            ),
            SizedBox(height: 10),
            ProfileListTile(
              img: AppImages.payment,
              title: S.of(context).payment,
            ),
            SizedBox(height: 10),
            ProfileListTile(
              img: AppImages.terms,
              title: S.of(context).terms_and_privacy_policy,
            ),
            SizedBox(height: 10),
            ProfileListTile(
              img: AppImages.support,
              title: S.of(context).support_help,
            ),
            SizedBox(height: 10),
            ProfileListTile(img: AppImages.about, title: S.of(context).about),
          ],
        ),
      ),
    );
  }
}
