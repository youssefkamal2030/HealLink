import 'package:flutter/material.dart';
import 'package:flutter_svg/svg.dart';
import 'package:heal_link/core/utils/app_images.dart';
import 'package:heal_link/core/utils/app_styles.dart';
import 'package:heal_link/core/utils/function/app_colors.dart';

class ProfileListTile extends StatelessWidget {
  const ProfileListTile({
    super.key,
    required this.img,
    required this.title,
    this.onTap,
  });

  final String img;
  final String title;
  final void Function()? onTap;

  @override
  Widget build(BuildContext context) {
    return InkWell(
      onTap: onTap,
      borderRadius: BorderRadius.circular(8),
      child: Container(
        height: 56,
        padding: const EdgeInsets.symmetric(horizontal: 8, vertical: 16),
        decoration: BoxDecoration(
          color: Colors.white,
          borderRadius: BorderRadius.circular(8),
          boxShadow: const [BoxShadow(color: Color(0x0000000D))],
        ),
        child: Row(
          spacing: 8,
          children: [
            SvgPicture.asset(img,),
            Text(title, style: AppTextStyles.popins400style14LightBlackColor),
            const Spacer(),
            SvgPicture.asset(AppImages.arrow, color: AppColors.kPrimaryColor),
          ],
        ),
      ),
    );
  }
}
