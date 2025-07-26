import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:flutter_svg/svg.dart';
import 'package:heal_link/core/utils/app_styles.dart';
import 'package:heal_link/core/utils/function/app_colors.dart';

class InfoField extends StatelessWidget {
  const InfoField({
    super.key,
    required this.label,
    required this.value,
    this.img,
    required this.isEditable,
  });

  final String label;
  final String value;
  final String? img;
  final bool isEditable;

  @override
  Widget build(BuildContext context) {
    return Container(
      height: 37,
      padding: EdgeInsets.all(8),
      decoration: BoxDecoration(
        border: Border.all(
          width: 1,
          color: Color.fromARGB(0, 0, 0, 0).withOpacity(0.05),
        ),
        borderRadius: BorderRadius.all(Radius.circular(8)),
      ),
      child: Row(
        spacing: 8,
        children: [
          Row(
            children: [
              RichText(
                text: TextSpan(
                  text: '$label: ',
                  style: AppTextStyles.popins500style14LightBlackColor,
                  children: [
                    TextSpan(
                      text: value,
                      style: AppTextStyles.popins400style14LightBlackColor
                          .copyWith(color: AppColors.kDarkGreyColor),
                    ),
                  ],
                ),
              ),
            ],
          ),

          Spacer(),
          if (isEditable && img != null)
            SvgPicture.asset(img!, height: 16, width: 16),
        ],
      ),
    );
  }
}
