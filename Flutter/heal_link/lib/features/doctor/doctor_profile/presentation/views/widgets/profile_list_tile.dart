import 'package:flutter/material.dart';
import 'package:flutter_svg/svg.dart';
import 'package:heal_link/core/utils/app_styles.dart';
import 'package:heal_link/core/utils/function/app_colors.dart';

class ProfileListTile extends StatelessWidget {
  const ProfileListTile({
    super.key,
    required this.img,
    required this.title,
  });
  final String img;
  final String title;

  @override
  Widget build(BuildContext context) {
    return Container(
      decoration: BoxDecoration(
        color: Colors.white,
        borderRadius: BorderRadius.circular(8),
      ),
      child: ListTile(
        leading: SvgPicture.asset(img),
        title: Text(
          title,
          style: AppTextStyles.popins400style14LightBlackColor,
        ),
        trailing: Icon(Icons.arrow_forward_ios,color: AppColors.kPrimaryColor,),
      ),
    );
  }
}
