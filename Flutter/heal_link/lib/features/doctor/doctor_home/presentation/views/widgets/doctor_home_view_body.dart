import 'package:flutter/material.dart';
import 'package:go_router/go_router.dart';
import 'package:heal_link/core/utils/app_images.dart';
import 'package:heal_link/core/utils/app_router.dart';
import 'package:heal_link/core/utils/app_styles.dart';
import 'package:heal_link/core/widgets/custom_text_form_field2.dart';
import 'package:heal_link/features/doctor/doctor_home/presentation/views/widgets/patient_info_card.dart';
import 'package:heal_link/features/doctor/doctor_home/presentation/views/widgets/total_patient_widget.dart';
import '../../../../../../generated/l10n.dart';
import 'home_doctor_info.dart';

class DoctorHomeViewBody extends StatelessWidget {
  const DoctorHomeViewBody({super.key});

  @override
  Widget build(BuildContext context) {
    return CustomScrollView(
      slivers: [
        SliverToBoxAdapter(
          child: Column(
            children: [
              HomeDoctorInfo(),
              Padding(
                padding: const EdgeInsets.symmetric(horizontal: 16.0),
                child: Column(
                  children: [
                    SizedBox(height: 24),
                    CustomTextFormField2(
                      showCursor: false,
                      hintText: S.of(context).searchPatient,
                      keyboardType: TextInputType.none,
                      controller: null,
                      validator: (value) {
                        return null;
                      },
                      onTap: () {
                        context.push(AppRouter.doctorSearchView);
                      },
                      prefixIcon: AppImages.search,
                      borderRadiusSize: 8,
                    ),
                    SizedBox(height: 24),
                    TotalPatientWidget(),
                    SizedBox(height: 24),
                    Row(
                      children: [
                        Text(
                          S.of(context).patient_results,
                          style: AppTextStyles.popins500style20LightBlackColor,
                        ),
                        Spacer(),
                        InkWell(
                          onTap: () => context.push(AppRouter.allPatientsView),
                          child: Text(
                            S.of(context).see_all,
                            style: AppTextStyles.popins400style12kPrimaryColor,
                          ),
                        ),
                        SizedBox(width: 3),
                      ],
                    ),
                    SizedBox(height: 12),
                    // NoPatientAvailable(),
                  ],
                ),
              ),
            ],
          ),
        ),
        SliverList(
          delegate: SliverChildBuilderDelegate(
            (context, index) => Padding(
              padding: const EdgeInsets.symmetric(
                horizontal: 16.0,
                vertical: 4,
              ),
              child: PatientInfoCard(
                response: () => context.push(AppRouter.patientDetailsView),
              ),
            ),
            childCount: 3,
          ),
        ),
        SliverToBoxAdapter(child: SizedBox(height: 30)),
        SliverToBoxAdapter(child: SizedBox(height: 56)),
      ],
    );
  }
}
