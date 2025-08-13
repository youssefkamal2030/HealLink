import 'package:flutter/material.dart';
import 'package:heal_link/core/utils/app_styles.dart';
import 'package:heal_link/core/utils/function/app_colors.dart';
import 'package:heal_link/features/doctor/doctor_home/presentation/views/widgets/total_patient_tab_bar_body.dart';

import '../../../../../core/widgets/custom_app_bar_pop_widget.dart';
import '../../../../../generated/l10n.dart';

class TotalPatientView extends StatelessWidget {
  const TotalPatientView({super.key});

  @override
  Widget build(BuildContext context) {
    return DefaultTabController(
      length: 2,
      child: Scaffold(
        body: Padding(
          padding: const EdgeInsets.symmetric(horizontal: 16.0),
          child: Column(
            spacing: 24,
            children: [
              CustomAppBarPopWidget(text: S.of(context).total_patient),
              TabBar(
                labelStyle: AppTextStyles.popins500style18LightBlackColor
                    .copyWith(fontSize: 16, color: AppColors.kPrimaryColor),
                indicatorSize: TabBarIndicatorSize.tab,
                indicatorWeight: 3,
                indicatorColor: AppColors.kPrimaryColor,
                dividerHeight: 3,
                dividerColor: Color(0xffD9D9D9),
                unselectedLabelStyle: AppTextStyles
                    .popins500style18LightBlackColor
                    .copyWith(fontSize: 16, color: AppColors.kDarkGreyColor),
                tabs: [
                  Padding(padding: EdgeInsets.all(8), child: Text(S.of(context).members)),
                  Padding(padding: EdgeInsets.all(8), child: Text(S.of(context).requests)),
                ],
              ),
              TotalPatientTabBarBody(),
            ],
          ),
        ),
      ),
    );
  }
}

