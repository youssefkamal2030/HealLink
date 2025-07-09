import 'package:flutter/material.dart';

import '../../../../../../core/utils/app_styles.dart';
import '../../../../../../core/utils/constant.dart';
import '../../../../../../core/utils/function/app_colors.dart';
import '../../../../../../generated/l10n.dart';

class TotalPatientWidget extends StatelessWidget {
  const TotalPatientWidget({super.key});

  @override
  Widget build(BuildContext context) {
    return Container(
      height: 161,
      width: double.infinity,
      decoration: BoxDecoration(
        color: AppColors.kWhiteColor,
        borderRadius: BorderRadius.circular(8),
      ),
      padding: EdgeInsets.symmetric(horizontal: 8, vertical: 16),
      child: Column(
        spacing: 15,
        children: [
          Text(
            S.of(context).total_patient,
            style: AppTextStyles.popins500style20LightBlackColor,
          ),
          Row(
            spacing: 12,
            children: [
              Container(
                height: 83,
                width: (AppConstant.width - 60) / 2,
                decoration: BoxDecoration(
                  color: AppColors.kPrimaryColor,
                  borderRadius: BorderRadius.circular(8),
                ),
                padding: EdgeInsets.symmetric(vertical: 8),
                child: Column(
                  spacing: 15,
                  children: [
                    Text(
                      '400 +',
                      style: AppTextStyles.popins500style18LightBlackColor
                          .copyWith(color: AppColors.kWhiteColor, fontSize: 20),
                    ),
                    Text(
                      S.of(context).members,
                      style: AppTextStyles.popins400style14LightBlackColor
                          .copyWith(color: AppColors.kWhiteColor),
                    ),
                  ],
                ),
              ),
              Container(
                height: 83,
                width: (AppConstant.width - 60) / 2,
                decoration: BoxDecoration(
                  gradient: LinearGradient(
                    colors: [Color(0xffDBEBFF), Color(0xffFFFFFF)],
                    begin: AlignmentDirectional.centerEnd,
                    end: AlignmentDirectional.centerStart,
                  ),
                  borderRadius: BorderRadius.circular(8),
                ),
                padding: EdgeInsets.symmetric(vertical: 8),
                child: Column(
                  spacing: 15,
                  children: [
                    Text(
                      '4',
                      style: AppTextStyles.popins500style18LightBlackColor
                          .copyWith(fontSize: 20),
                    ),
                    Text(
                      S.of(context).requests,
                      style: AppTextStyles.popins400style14LightBlackColor,
                    ),
                  ],
                ),
              ),
            ],
          ),
        ],
      ),
    );
  }
}
