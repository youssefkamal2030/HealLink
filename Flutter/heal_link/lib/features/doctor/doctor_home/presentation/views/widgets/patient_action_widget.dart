import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:heal_link/core/utils/function/app_colors.dart';
import '../../../../../../core/utils/app_styles.dart';
import '../../../../../../core/utils/constant.dart';

class PatientActionWidget extends StatelessWidget {
  const PatientActionWidget({
    super.key,
    required this.text,
    required this.image,
    this.onTap,
  });

  final String text;
  final String image;
  final void Function()? onTap;

  @override
  Widget build(BuildContext context) {

    return InkWell(
      onTap:  onTap,
      child: Container(
        height: 96,
        width: (AppConstant.width - 48) / 2,
        decoration: BoxDecoration(
          color: AppColors.kWhiteColor,
          borderRadius: BorderRadius.circular(8),
          boxShadow: [
            BoxShadow(
              offset: Offset(1, 1),
              blurRadius: 4,
              color: Color(0x12000000),
            ),
          ],
        ),
        padding: EdgeInsets.symmetric(vertical: 8),
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          spacing: 8,
          children: [
            SvgPicture.asset(image),
            Text(text, style: AppTextStyles.popins600style14PrimaryColor),
          ],
        ),
      ),
    );
  }
}
